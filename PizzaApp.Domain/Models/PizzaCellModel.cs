namespace PizzaApp.Domain.Models
{
    public abstract class PizzaCellModel : ClassBaseModel
    {
        public PizzaModel Pizza { get; set; }
        public bool IsFavorite { get; set; }

        public string ImageSource
        {
            get {
                return this.IsFavorite ? "star2.png"
                                       : "star1.png";
            }
        }

        public PizzaCellModel()
        {
        }
    }
}