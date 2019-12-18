using PizzaApp.Domain.Models;
using System.Threading.Tasks;

namespace PizzaApp.Domain.Interfaces
{
    public interface IUserPreferenceRepository
    {
        Task<UserPreferenceModel> Get();

        Task Set(UserPreferenceModel userPreference);
    }
}