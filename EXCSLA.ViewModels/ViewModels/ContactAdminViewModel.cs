using System.Collections.Generic;
using EXCSLA.Models;
namespace EXCSLA.ViewModels
{
    public class ContactAdminViewModel
    {
        public IEnumerable<Contact> Contacts {get; set;}
        public IEnumerable<ContactReason> ContactReasons {get; set;}
    }
}