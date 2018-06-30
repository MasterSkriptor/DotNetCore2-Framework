namespace EXCSLA.Models
{
    public class ConfirmDelete
    {
        private bool _confirm;
        private int _id;

        public bool Confirm {get{return _confirm;} set{_confirm = value;}}
        public int Id {get{return _id;} set{_id = value;}}

        public ConfirmDelete()
        {
            
        }
    }
}