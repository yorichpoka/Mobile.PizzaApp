using System.Collections.Generic;

namespace PizzaApp.Domain.Models
{
    public class PizzaModel : ClassBaseModel
    {
        public string ImageUrl { get; set; }
        public string Nom { get; set; }
        public int Prix { get; set; }
        public List<string> Ingredients { get; set; }

        // ToString object
        public string ToStringPrix
        {
            get {
                return $"{this.Prix} €";
            }
        }

        public string ToStringIngredients
        {
            get {
                return this.Ingredients != null ? string.Join(", ", this.Ingredients)
                                                : string.Empty;
            }
        }

        public string ToStringNom
        {
            get {
                return this.Nom != null ? this.Nom.ExtUppercaseFirstChar()
                                        : string.Empty;
            }
        }

        public PizzaModel() { }
    }
}