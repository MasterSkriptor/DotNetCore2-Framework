using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXCSLA.Models
{
    public class ContactReason : Entity<int>, IEntity
    {
        #region Variables
        private string _emailTo;

        #endregion

        #region Properties
        
        [Required][EmailAddress]
        public string EmailTo { get { return _emailTo;} set { _emailTo = value;} }
        
        #endregion

        #region Constructors
        public ContactReason()
        {
            
        }

        #endregion
    }
}