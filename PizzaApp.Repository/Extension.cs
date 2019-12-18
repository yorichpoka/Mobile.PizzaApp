using PizzaApp.Repository.Entities;

namespace PizzaApp.Repository
{
    public static class Extension
    {
        public static void ExtUpdate(this UserPreference value, UserPreference obj)
        {
            value.Tri = obj.Tri;
            value.Favoris = obj.Favoris;
        }
    }
}