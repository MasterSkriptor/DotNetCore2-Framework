using System;

namespace EXCSLA.Models
{
    public abstract class EntityChild<P, C> : IEntityChild<P, C>
    {
        public P ParentId {get; set;}
        object IEntityChild.ParentId
        {
            get { return this.ParentId; }
            set { this.ParentId = (P)Convert.ChangeType(value, typeof(P)); }
        }

        public C ChildId {get; set;}
        object IEntityChild.ChildId
        {
            get { return this.ChildId; }
            set { this.ChildId = (C)Convert.ChangeType(value, typeof(C)); }
        }
    }
}