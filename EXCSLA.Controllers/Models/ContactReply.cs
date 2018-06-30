using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXCSLA.Models
{
    public class ContactReply : Entity<int>, IEntity
    {
        private string _userId;
        private string _message;
        private int _contactId;

        public string UserId{ get{ return _userId;} set{ _userId = value; } }
        public string Message{get{return _message;} set{_message = value;}}
        public int ContactId { get{ return _contactId;} set {_contactId = value; } }
        
        [NotMapped]
        public DateTime DateSent {get{return base.CreatedDate; }set{base.CreatedDate = value;}}
        public virtual Contact Contact {get; set;}
        public ContactReply()
        {
            
        }
    }
}