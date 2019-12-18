using PizzaApp.Domain.Enums;
using PizzaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaApp.Domain.Interfaces
{
    public interface IPizzaBussiness
    {
        Task<KeyValuePair<Boolean, List<PizzaModel>>> GetFromTri(ETri tri, string jsonFileName, List<string> favorisList = null);
    }
}