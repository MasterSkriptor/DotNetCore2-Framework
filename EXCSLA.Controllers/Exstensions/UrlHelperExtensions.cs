using Microsoft.AspNetCore.Mvc;
using EXCSLA.Controllers;
using EXCSLA.Services.DataServices;
using EXCSLA.Services.EmailServices;
using Microsoft.AspNetCore.Identity;

namespace EXCSLA.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string ReplyToContactLink(this IUrlHelper urlHelper, int contactId, string scheme)
        {
            return urlHelper.Action(
                action: nameof(ContactControllerBase<IDataService, IEmailSender, IdentityUser>.Reply),
                controller: "Contact",
                values: new { id = contactId },
                protocol: scheme);
        }
    }
}