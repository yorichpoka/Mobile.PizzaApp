using PizzaApp.Domain.Models;
using PizzaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaApp
{
    public static class Extension
    {
        public static List<PizzaCellViewModel> ExtConvertTo(this List<PizzaModel> value, List<string> favorites, Action<PizzaCellViewModel> onFavoriteChangeAction)
        {
            // Set array list of favorite lower
            favorites = favorites.Select(fav => fav.ToLower()).ToList();

            return
                value.Select(l =>
                        new PizzaCellViewModel(l, onFavoriteChangeAction)
                        {
                            IsFavorite = favorites.Contains(l.ToStringNom.ToLower())
                        }
                     )
                     .ToList();
        }
    }
}