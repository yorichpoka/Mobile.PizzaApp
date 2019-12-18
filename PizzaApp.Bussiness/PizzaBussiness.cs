using PizzaApp.Domain.Enums;
using PizzaApp.Domain.Interfaces;
using PizzaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaApp.Bussiness
{
    public class PizzaBussiness : IPizzaBussiness
    {
        private readonly IPizzaRepository _PizzaRepository;

        public PizzaBussiness(IPizzaRepository pizzaRepository)
        {
            this._PizzaRepository = pizzaRepository;
        }

        public async Task<KeyValuePair<Boolean, List<PizzaModel>>> GetFromTri(ETri tri, string jsonFileName, List<string> favorisList = null)
        {
            var results = await this._PizzaRepository.Get(jsonFileName);
            var sortedList = new List<PizzaModel>();

            if (tri == ETri.Nom)
            {
                sortedList = results.Value.OrderBy(l => l.ToStringNom)
                                          .ToList();
            }
            else if (tri == ETri.Prix)
            {
                sortedList = results.Value.OrderBy(l => l.Prix)
                                          .ToList();
            }
            else if (tri == ETri.Favoris && favorisList != null)
            {
                favorisList.ForEach(l => l = l.ToLower());
                sortedList = results.Value.Where(l => favorisList.Contains(l.ToStringNom.ToLower()))
                                          .ToList();
            }
            else
            {
                sortedList = results.Value;
            }

            return new KeyValuePair<bool, List<PizzaModel>>(results.Key, sortedList);
        }
    }
}