namespace PizzaApp.Repository.Entities
{
    public class UserPreference : ClassBaseEntity
    {
        public int Tri { get; set; }
        public string Favoris { get; set; }

        public UserPreference()
        {
        }
    }
}