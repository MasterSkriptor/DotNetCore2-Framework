using System;

namespace EXCSLA.Models
{
    public abstract class WebObjectBase : Entity<int>, IWebObject
    {
        #region Variables
        private bool _showOnSite;
        private bool _markedForDeletion;
        #endregion

        #region Properties
        public bool ShowOnSite { get { return _showOnSite;} set { _showOnSite = value;} }
        public bool MarkedForDeletion { get { return _markedForDeletion;} set { _markedForDeletion = value;} }

        #endregion

        #region Constructors
        public WebObjectBase() { }
        #endregion        
    }
}