using System.Collections.Generic;
using EXCSLA.Models;

namespace EXCSLA.ViewModels
{
    public class ContactEditViewModel
    {
        public Contact Contact {get; set;}
        public IEnumerable<ContactReply> Replies {get; set;}

    }
}