using PizzaApp.Domain.Models;
using System.Threading.Tasks;

namespace PizzaApp.Domain.Interfaces
{
    public interface IUserPreferenceBussiness
    {
        Task<UserPreferenceModel> Get();

        Task Set(UserPreferenceModel userPreference);
    }
}