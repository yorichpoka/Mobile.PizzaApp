using PizzaApp.Domain.Interfaces;
using PizzaApp.Domain.Models;
using System.Threading.Tasks;

namespace PizzaApp.Bussiness
{
    public class UserPreferenceBussiness : IUserPreferenceBussiness
    {
        private readonly IUserPreferenceRepository _Repository;

        public UserPreferenceBussiness(IUserPreferenceRepository repository)
        {
            this._Repository = repository;
        }

        public Task<UserPreferenceModel> Get()
        {
            return this._Repository.Get();
        }

        public Task Set(UserPreferenceModel userPreference)
        {
            return this._Repository.Set(userPreference);
        }
    }
}