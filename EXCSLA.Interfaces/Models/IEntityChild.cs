namespace EXCSLA.Models
{
    public interface IEntityChild
    {
        object ParentId {get; set;}
        object ChildId{get; set;}
    }

    public interface IEntityChild<P, C> : IEntityChild
    {
        new P ParentId {get; set;}
        new C ChildId {get; set;}
    }
}