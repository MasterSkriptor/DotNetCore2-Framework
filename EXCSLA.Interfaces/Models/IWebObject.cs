namespace EXCSLA.Models
{
    public interface IWebObject : IEntity<int>
    {
        bool ShowOnSite {get; set;}
        bool MarkedForDeletion {get; set;}
    }

}