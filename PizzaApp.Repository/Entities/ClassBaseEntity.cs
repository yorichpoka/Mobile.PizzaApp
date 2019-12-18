using SQLite;

namespace PizzaApp.Repository.Entities
{
    public abstract class ClassBaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}