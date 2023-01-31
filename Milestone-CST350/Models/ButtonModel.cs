namespace ButtonGrid.Models
{
    public class ButtonModel
    {
        public int Id { get; set; }
        public bool Clicked { get; set; }
        public bool Flagged { get; set; }

        public ButtonModel() { }

        public ButtonModel(int id, bool clicked, bool flagged) 
        { 
            this.Id = id;
            this.Clicked = clicked;
            this.Flagged = Flagged;
        }

    }
}
