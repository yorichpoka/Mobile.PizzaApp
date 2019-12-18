namespace PizzaApp.Repository.Entities
{
    public class Pizza : ClassBaseEntity
    {
        public string ImageUrl { get; set; }
        public string Nom { get; set; }
        public int Prix { get; set; }
        public string Ingredients { get; set; }

        public Pizza()
        {
        }
    }
}