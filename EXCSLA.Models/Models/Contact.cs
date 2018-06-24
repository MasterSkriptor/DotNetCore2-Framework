using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXCSLA.Models
{
    public class Contact : Entity<int>, IEntity
    {
        #region variables
        private string _firstName;
        private string _lastName;
        private string _phone;
        private string _message;
        private int _contactReasonId;
        private bool _handled;



        #endregion

        #region properties

        [Required]
        public string FirstName { get { return _firstName;} set { _firstName = value;} }

        [Required]
        public string LastName { get { return _lastName;} set { _lastName = value;} }

        [Required][EmailAddress][NotMapped]
        public string Email { get { return base.Name;} set { base.Name = value;} }

        [DataType(DataType.PhoneNumber)][Phone]
        public string Phone {get {return _phone; } set{_phone = value;}}

        [Required][NotMapped]
        public DateTime TimeStamp { get { return base.CreatedDate; } set { base.CreatedDate = value; } }

        public bool Handled { get { return _handled; } set{ _handled = value; } }

        public int ContactReasonId { get { return _contactReasonId;} set { _contactReasonId = value;} }    
        public virtual ContactReason ContactReason {get; set;}

        [Required]
        public string Message { get { return _message; } set { _message = value; } }
        #endregion

        #region constructors
        public Contact()
        {
            _handled = false;
        }
        #endregion
    }
}