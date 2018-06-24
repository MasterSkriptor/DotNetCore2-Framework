using System;
using EXCSLA.Models;

namespace EXCSLA.ViewModels
{
    public class ContactReplyViewModel
    {
        private Contact _contact;

        private int _contactID;
        private bool _hideEmail;
        
        private string _userName;
        private string _message;
        private DateTime _date;
        
        public Contact Contact { get{ return _contact;} set{ _contact = value;}}
        public int ContactId {get{return _contactID;} set {_contactID = value;} }
        public bool HideEmail{ get{return _hideEmail;} set{_hideEmail = value;}}

        public string UserName {get{ return _userName;} set{_userName = value;} }
        public string Message {get {return _message;} set{ _message = value;} }
        public DateTime DateSent {get{return _date;} set{_date = value;}}
    }
}