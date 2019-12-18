using PizzaApp.Domain.Enums;
using System.Collections.Generic;

namespace PizzaApp.Domain.Models
{
    public class UserPreferenceModel : ClassBaseModel
    {
        public ETri Tri { get; set; }
        public List<string> Favoris { get; set; }

        public UserPreferenceModel()
        {
            this.Tri = ETri.Aucun;
            this.Favoris = new List<string>();
        }

        public void SetTri()
        {
            if (this.Tri == ETri.Aucun)
                this.Tri = ETri.Nom;
            else if (Tri == ETri.Nom)
                this.Tri = ETri.Prix;
            else if (Tri == ETri.Prix)
                this.Tri = ETri.Favoris;
            else
                this.Tri = ETri.Aucun;
        }

        public void SetFavoris(PizzaCellModel pizzaCell)
        {
            if (this.Favoris.Contains(pizzaCell.Pizza.ToStringNom.ToLower()))
            {
                if (!pizzaCell.IsFavorite)
                {
                    this.Favoris.Remove(pizzaCell.Pizza.ToStringNom.ToLower());
                }
            }
            else
            {
                if (pizzaCell.IsFavorite)
                {
                    this.Favoris.Add(pizzaCell.Pizza.ToStringNom.ToLower());
                }
            }
        }
    }
}