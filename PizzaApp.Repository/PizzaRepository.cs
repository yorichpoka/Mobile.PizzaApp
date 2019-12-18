using Newtonsoft.Json;
using PizzaApp.Domain.Interfaces;
using PizzaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PizzaApp.Repository
{
    public class PizzaRepository : IPizzaRepository
    {
        private InputParameterModel _InputParameter { get; set; }

        public PizzaRepository(InputParameterModel inputParameter)
        {
            this._InputParameter = inputParameter;
        }

        public Task<KeyValuePair<Boolean, List<PizzaModel>>> Get(string jsonFileName)
        {
            return
                Task.Factory.StartNew<KeyValuePair<Boolean, List<PizzaModel>>>(
                    () => {
                        // Request to retrieve data
                        using (var httpClient = new HttpClient())
                        {
                            // Get data from local
                            string jsonData = string.Empty;
                            Boolean isRequestSuccess = true;

                            try
                            {
                                // Execute reuest
                                HttpResponseMessage response = httpClient.GetAsync(this._InputParameter.UrlRequestUri).Result;

                                // Check if success
                                if (response.StatusCode != HttpStatusCode.OK)
                                    throw new Exception("Data was not retrieved!");

                                // Get json data
                                jsonData = response.Content.ReadAsStringAsync().Result;

                                // Save in file
                                SetFromFile(jsonFileName, jsonData);
                            }
                            catch
                            {
                                // Definerequest success
                                isRequestSuccess = false;
                                // Get data from local
                                jsonData = GetFromFile(jsonFileName);
                            }

                            // Convert and return json data
                            return
                                new KeyValuePair<bool, List<PizzaModel>>(
                                    isRequestSuccess,
                                    JsonConvert.DeserializeObject<List<PizzaModel>>(jsonData)
                                );
                        }
                    }
                );

            #region Static values

            /*
            return
                new List<PizzaModel>() {
                    new PizzaModel
                    {
                        Nom = "Végétarienne",
                        ImageUrl = "https://media-cdn.tripadvisor.com/media/photo-s/15/c5/a4/14/pepperoni-lovers.jpg",
                        Prix = 7,
                        Ingredients = new List<string>
                        {
                            "Tomate",
                            "Poivron",
                            "Oignons"
                        }
                    },
                    new PizzaModel
                    {
                        Nom = "montagnarde",
                        ImageUrl = "https://www.kyran-o-pizza.com/ressources/images/904a5cc77ec2.jpg",
                        Prix = 11,
                        Ingredients = new List<string>
                        {
                            "Reblonchon",
                            "Pomme de terre",
                            "Oignons",
                            "Crème",
                            "Reblonchon",
                            "Pomme de terre",
                            "Oignons",
                            "Crème"
                        }
                    },
                    new PizzaModel
                    {
                        Nom = "CARNIVORE",
                        ImageUrl = "https://www.monsieur-cuisine.com/fileadmin/_processed_/b/b/csm_23148_Rezeptfoto_01_cd6aec6c0e.jpg",
                        Prix = 14,
                        Ingredients = new List<string>
                        {
                            "Tomate",
                            "Viande hachée",
                            "Mozzarella"
                        }
                    }
                };
            */

            #endregion Static values
        }

        /// <summary>
        /// Get values from file
        /// </summary>
        /// <param name="jsonFileName"></param>
        /// <returns></returns>
        private string GetFromFile(string jsonFileName)
        {
            if (File.Exists(jsonFileName))
                return File.ReadAllText(jsonFileName);
            else
                // Init file
                SetFromFile(jsonFileName, "[]");

            return "[]";
        }

        /// <summary>
        /// Value value from file
        /// </summary>
        /// <param name="jsonFileName"></param>
        /// <param name="jsonValue"></param>
        private void SetFromFile(string jsonFileName, string jsonValue)
        {
            File.WriteAllText(jsonFileName, jsonValue);
        }
    }
}