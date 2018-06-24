using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EXCSLA.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EXCSLA.ViewModels
{
    public class ContactViewModel
    {
        #region variables
        private IEnumerable<SelectListItem> _contactReasons;
        private string _lastName;
        private string _firstName;
        private string _email;
        private int _contactReason;
        private string _message;

        #endregion

        #region properties
        public IEnumerable<SelectListItem> ContactReasons { get { return _contactReasons; } private set { _contactReasons = value; } }
        [DisplayName("First Name")][Required]
        public string FirstName { get { return _firstName; } set { _firstName = value; } }
        [DisplayName("Last Name")]
        [Required]
        public string LastName { get { return _lastName; } set { _lastName = value; } }
        [DisplayName("Email")]
        [Required][EmailAddress]
        public string Email { get { return _email; } set { _email = value; } }
        [DisplayName("Contact Reason")]
        [Required]
        public int ContactReason { get { return _contactReason; } set { _contactReason = value; } }
        [Required]
        public string Message { get { return _message; } set { _message = value; } }


        #endregion

        #region constructors
        public ContactViewModel()
        {
        }
        #endregion

        #region methods
        public void SetContactReasons(IEnumerable<ContactReason> contactReasons)
        {
            var returnReasons = new List<SelectListItem>();

            foreach (ContactReason reason in contactReasons)
            {
                returnReasons.Add(new SelectListItem
                {
                    Value = reason.Id.ToString(),
                    Text = reason.Name
                });
            }

            _contactReasons = returnReasons;
        }
        #endregion
    }
}