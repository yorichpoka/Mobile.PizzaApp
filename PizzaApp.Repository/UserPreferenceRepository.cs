using AutoMapper;
using PizzaApp.Domain.Enums;
using PizzaApp.Domain.Interfaces;
using PizzaApp.Domain.Models;
using PizzaApp.Repository.Entities;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaApp.Repository
{
    public class UserPreferenceRepository : IUserPreferenceRepository
    {
        private SQLiteAsyncConnection _Database { get; set; }
        private readonly IMapper _Mapper;

        public UserPreferenceRepository(SQLiteAsyncConnection database, IMapper mapper)
        {
            this._Database = database;
            this._Mapper = mapper;
        }

        public Task<UserPreferenceModel> Get()
        {
            return
                Task.Factory.StartNew<UserPreferenceModel>(
                    () => {
                        // Get value from data base
                        var entity = this._Database.Table<UserPreference>().FirstOrDefaultAsync().Result;

                        if (entity == null)
                            return new UserPreferenceModel();

                        // Map data
                        var userModel = this._Mapper.Map<UserPreference, UserPreferenceModel>(entity);

                        return userModel;
                    }
                );
        }

        public Task Set(UserPreferenceModel userPreference)
        {
            return
                Task.Factory.StartNew(
                    () => {
                        // Get old data
                        var entity = this._Database.Table<UserPreference>().FirstOrDefaultAsync().Result;

                        // Clear table
                        if (entity == null)
                        {
                            this._Database.InsertAsync(
                                this._Mapper.Map<UserPreferenceModel, UserPreference>(userPreference)
                            )
                            .Wait();
                        }
                        else
                        {
                            entity.ExtUpdate(
                                this._Mapper.Map<UserPreferenceModel, UserPreference>(userPreference)
                            );

                            this._Database.UpdateAsync(entity)
                                          .Wait();
                        }
                    }
                );
        }
    }
}