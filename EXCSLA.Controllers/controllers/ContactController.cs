using System.Threading.Tasks;
using System.Linq;
using EXCSLA.Models;
using EXCSLA.Services.DataServices;
using EXCSLA.Services.EmailServices;
using EXCSLA.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using EXCSLA.Extensions;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace EXCSLA.Controllers
{
    public abstract class ContactControllerBase<TContext, TEmailSender, TIdentityUser> : Controller
        where TContext : IDataService
        where TEmailSender : IEmailSender
        where TIdentityUser : IdentityUser
    {
        private TContext _context;
        private TEmailSender _emailSender;
        private UserManager<TIdentityUser> _userManager;
        private IConfiguration _configuration;

        public ContactControllerBase(TContext context, TEmailSender emailSender, 
            UserManager<TIdentityUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _emailSender = emailSender;
            _userManager = userManager;
            _configuration = configuration;
        }

        [Authorize(Roles = "Administrator")]
        public virtual async Task<IActionResult> Admin()
        {
            var viewModel = new ContactAdminViewModel();
            viewModel.Contacts = await _context.GetAllAsync<Contact>();
            viewModel.ContactReasons = await _context.GetAllAsync<ContactReason>();
            
            return View(viewModel);
        }

        [Authorize(Roles = "Administrator")]
        public virtual async Task<IActionResult> Edit(int? id)
        {
            if(id == null) return NotFound();

            var viewModel = new ContactEditViewModel();
            
            viewModel.Contact = await _context.GetByIdAsync<Contact>(id);
            viewModel.Replies = await _context.GetAsync<ContactReply>(m => m.ContactId == id);
            return View(viewModel);
        }

        [Authorize(Roles = "Administrator")]
        public virtual async Task<IActionResult> Reply(int? id)
        {
            if(id == null) return NotFound();

            var model = await _context.GetByIdAsync<Contact>(id);

            var viewModel = new ContactReplyViewModel();

            viewModel.ContactId = id.Value;
            viewModel.Contact = model;
            viewModel.DateSent = DateTime.Now;

            return View(viewModel);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost][ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Reply(int? id, ContactReplyViewModel model)
        {
            string subject;
            string returnEmail;

            if(id==null) return NotFound();
            if(model == null) return NotFound();
            if(model.Contact == null) model.Contact = await _context.GetByIdAsync<Contact>(id);
            if(id != model.ContactId) return NotFound();

            // Get current users email address
            var user = await _userManager.GetUserAsync(this.User);
            model.UserName = user.Email;
            
            // Get the subject from configuration, or use default
            if(!string.IsNullOrEmpty(_configuration["WebsiteContactSubject"]))
                subject = _configuration["WebsiteContactSubject"];
            else
                subject = "Website Contact";

            // Get the return email for when email addresses are hidden from configuration
            if(!string.IsNullOrEmpty(_configuration["WebsiteContactReturnEmail"]))
                returnEmail = _configuration["WebsiteContactReturnEmail"];
            else
                throw new KeyNotFoundException();

            // Save to the data context
            _context.Create<ContactReply>(new ContactReply {
                ContactId = model.ContactId,
                UserId = model.UserName,
                Message = model.Message
            });

            // Send the email
            if(model.HideEmail != true)
                await _emailSender.SendEmailAsync(model.Contact.Email, subject, model.Message, model.UserName);
            else
                await _emailSender.SendEmailAsync(model.Contact.Email, subject, model.Message, returnEmail);
            
            // Mark original Contact as Handled
            model.Contact.Handled = true;
            _context.Update(model.Contact);

            // Save changes to db
            await _context.SaveAsync();

            return RedirectToAction("Admin");
        }

        [AllowAnonymous]
        public virtual async Task<IActionResult> Index()
        {
            ViewData["Message"] = "Your contact page.";
            var model = new ContactViewModel();
            model.SetContactReasons(await _context.GetAllAsync<ContactReason>());
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost][AutoValidateAntiforgeryToken]
        public virtual async Task<IActionResult> Index(ContactViewModel viewModel)
        {
            // In case ModelState is invalid
            viewModel.SetContactReasons(await _context.GetAllAsync<ContactReason>());

            // Create new contact and add it to the database
            Contact contact = new Models.Contact();
            contact.FirstName = viewModel.FirstName;
            contact.LastName = viewModel.LastName;
            contact.Email = viewModel.Email;
            contact.ContactReasonId = viewModel.ContactReason;
            contact.Message = viewModel.Message;
            contact.TimeStamp = DateTime.Now;
            
            _context.Create<Contact>(contact);
            await _context.SaveAsync();

            // TODO: Send an email and post a note to the site that the contact was sent.
            contact = await _context.GetLastAsync<Contact>();
            string link = Url.ReplyToContactLink(contact.Id, Request.Scheme);
            await SendContactEmail(contact, link);

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Administrator")]
        public virtual IActionResult ContactReasonAdd()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost][ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> ContactReasonAdd([Bind("Name, EmailTo")]ContactReason reason)
        {
            _context.Create<ContactReason>(reason);
            await _context.SaveAsync();

            return RedirectToAction("Admin");
        }

        [Authorize(Roles = "Administrator")]
        public virtual async Task<IActionResult> ContactReasonEdit(int? id)
        {
            if(id == null || id < 0)
                return NotFound();
            
            var model = await _context.GetByIdAsync<ContactReason>(id);

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost][ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> ContactReasonEdit(int? id, [Bind("Id, Name, EmailTo, Version")] ContactReason reason)
        {
            if(id == null || id != reason.Id)
                return NotFound();
            
            _context.Update<ContactReason>(reason);
            await _context.SaveAsync();

            return RedirectToAction("Admin");
        }

        [Authorize(Roles = "Administrator")]
        public virtual async Task<IActionResult> ContactReasonDelete(int? id)
        {
            if (id == null || id < 0)
                return NotFound();

            var model = await _context.GetByIdAsync<ContactReason>(id);

            if (model == null)
                return NotFound();

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("ContactReasonDelete")] [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> ContactReasonDeleteConfirmed(int? id)
        {
            if (id == null || id < 0)
                return NotFound();

            var reason = await _context.GetByIdAsync<ContactReason>(id);
            if (reason == null)
                return NotFound();

            _context.Delete<ContactReason>(reason);
            await _context.SaveAsync();

            return RedirectToAction("Admin");
        }

        [Authorize(Roles = "Administrator")]
        public virtual IActionResult HandleContact(int? id)
        {
            if(id == null) return NotFound();

            var model = new ConfirmDelete();
            model.Id = id.Value;

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost][ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> HandleContact(int? id, bool confirm)
        {
            if(id == null) return NotFound();

            if(confirm == true) 
            {
                var contact = await _context.GetByIdAsync<Contact>(id);
                contact.Handled = true;

                _context.Update<Contact>(contact);
                await _context.SaveAsync();
            }

            return RedirectToAction("Admin");
        }

        [Authorize(Roles = "Administrator")]
        public virtual IActionResult FlushHandledContacts()
        {
            var model = new ConfirmDelete();

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost][ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> FlushHandledContacts(bool confirm)
        {
            if(confirm == true)
            {
                var contactsToFlush = await _context.GetAsync<Contact>(m => m.Handled == true);

                foreach(var contact in contactsToFlush)
                {
                    IEnumerable<ContactReply> replies = await _context.GetAsync<ContactReply>(m => m.ContactId == contact.Id);
                    foreach(var reply in replies)
                    {
                        _context.Delete<ContactReply>(reply);
                    }
                    _context.Delete<Contact>(contact);
                }

                await _context.SaveAsync();
            }

            return RedirectToAction("Admin");
        }

        public virtual async Task SendContactEmail(Contact contact, string replyUrl)
        {
            // Send email to the person responsible for the contact
            string email = contact.ContactReason.EmailTo;
            string subject = "MamaTara Website Contact";
            string message = GetMessageString(contact, replyUrl);

            await _emailSender.SendEmailAsync(email, subject, message);

            // Send a thank you / acknowledgement of the contact to
            // to the person who made the request.
        }

        protected virtual string GetMessageString(Contact contact, string replyUrl)
        {
            StringBuilder messageToReturn = new StringBuilder();
            
            messageToReturn.AppendLine("<h3>Website Contact Recieved</h3>");
            messageToReturn.AppendLine($"<p>You have recieved a request from {contact.FirstName} {contact.LastName}.</p>");
            messageToReturn.AppendLine("<p>The contact information is as follows:");
            messageToReturn.AppendLine($"<b>First Name:</b> {contact.FirstName}</br>");
            messageToReturn.AppendLine($"<b>Last Name:</b> {contact.LastName}</br>");
            messageToReturn.AppendLine($"<b>Email:</b> {contact.Email}</br>");
            if(!string.IsNullOrEmpty(contact.Phone))
                messageToReturn.AppendLine($"<b>Phone Number:</b> {contact.Phone}</br>");
            messageToReturn.AppendLine($"<b>Message:</b></br> {contact.Message}</p>");
             
            // Add a line for the response link.
            messageToReturn.AppendLine($"click <a href='{replyUrl}'>here</a> to reply.");

            return messageToReturn.ToString();
        }
    }
}