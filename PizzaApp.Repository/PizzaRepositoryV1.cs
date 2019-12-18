using AutoMapper;
using Newtonsoft.Json;
using PizzaApp.Domain.Interfaces;
using PizzaApp.Domain.Models;
using PizzaApp.Repository.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PizzaApp.Repository
{
    public class PizzaRepositoryV1 : IPizzaRepository
    {
        private InputParameterModel _InputParameter { get; set; }
        private SQLiteAsyncConnection _Database { get; set; }
        private readonly IMapper _Mapper;

        public PizzaRepositoryV1(InputParameterModel inputParameter, SQLiteAsyncConnection database, IMapper mapper)
        {
            this._InputParameter = inputParameter;
            this._Database = database;
            this._Mapper = mapper;
        }

        public Task<KeyValuePair<Boolean, List<PizzaModel>>> Get(string jsonFileName = null)
        {
            return
                Task.Factory.StartNew<KeyValuePair<Boolean, List<PizzaModel>>>(
                    () => {
                        // Request to retrieve data
                        using (var httpClient = new HttpClient())
                        {
                            // Get data from local
                            List<PizzaModel> dataList = new List<PizzaModel>();
                            Boolean isRequestSuccess = true;

                            try
                            {
                                // Execute reuest
                                HttpResponseMessage response = httpClient.GetAsync(this._InputParameter.UrlRequestUri).Result;

                                // Check if success
                                if (response.StatusCode != HttpStatusCode.OK)
                                    throw new Exception("Data was not retrieved!");

                                // Get json data
                                var jsonData = response.Content.ReadAsStringAsync().Result;

                                // Save in file
                                dataList = SetFromDataBase(jsonData).Result;
                            }
                            catch (Exception ex)
                            {
                                // Definerequest success
                                isRequestSuccess = false;
                                // Get data from local
                                dataList = GetFromDataBase().Result;
                            }

                            // Convert and return json data
                            return
                                new KeyValuePair<bool, List<PizzaModel>>(isRequestSuccess, dataList);
                        }
                    }
                );
        }

        /// <summary>
        /// Gat values from data base
        /// </summary>
        /// <param name="jsonFileName"></param>
        /// <returns></returns>
        private Task<List<PizzaModel>> GetFromDataBase()
        {
            return
                Task.Factory.StartNew<List<PizzaModel>>(
                    () => {
                        var entities = this._Database.Table<Pizza>().ToListAsync().Result;

                        // Map data
                        var results = this._Mapper.Map<List<Pizza>, List<PizzaModel>>(entities);

                        return results;
                    }
                );
        }

        /// <summary>
        /// Set value from data base
        /// </summary>
        /// <param name="jsonFileName"></param>
        /// <param name="jsonValue"></param>
        private Task<List<PizzaModel>> SetFromDataBase(string jsonValue)
        {
            return
                Task.Factory.StartNew<List<PizzaModel>>(
                    () => {
                        // Cast json values
                        var results = JsonConvert.DeserializeObject<List<PizzaModel>>(jsonValue);

                        // Map data
                        var entities = this._Mapper.Map<List<PizzaModel>, List<Pizza>>(results);

                        // Clear table
                        this._Database.Table<Pizza>().DeleteAsync(l => l.Id != 0).Wait();

                        // Isert new daa
                        this._Database.InsertAllAsync(entities).Wait();

                        return results;
                    }
                );
        }
    }
}