using PizzaApp.Domain.Enums;

namespace PizzaApp.Domain
{
    public static class Extension
    {
        public static string ExtUppercaseFirstChar(this string value)
        {
            return string.IsNullOrEmpty(value) ? value
                                               : $"{value.ToUpper().Substring(0, 1)}{value.ToLower().Substring(1)}";
        }

        public static string ExtGetImageSource(this ETri eTri)
        {
            switch (eTri)
            {
                case ETri.Aucun:
                    return "sort_none.png";

                case ETri.Nom:
                    return "sort_nom.png";

                case ETri.Favoris:
                    return "sort_fav.png";

                default:
                    return "sort_prix.png";
            }
        }
    }
}