using PizzaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaApp.Domain.Interfaces
{
    public interface IPizzaRepository
    {
        Task<KeyValuePair<Boolean, List<PizzaModel>>> Get(string jsonFileName);
    }
}