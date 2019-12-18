using AutoMapper;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using PizzaApp.Bussiness;
using PizzaApp.Domain.Enums;
using PizzaApp.Domain.Interfaces;
using PizzaApp.Domain.Models;
using PizzaApp.Repository;
using PizzaApp.Repository.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;

namespace PizzaApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            RegisterDependencies();

            var navigationPage = new NavigationPage(new MainPage());
            navigationPage.BarBackgroundColor = Color.FromHex("#1abbd4");

            MainPage = navigationPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void RegisterDependencies()
        {
            // Create container
            var unityContainer = new UnityContainer();
            // Register new instance (Optional)
            unityContainer.RegisterInstance<InputParameterModel>(
                new InputParameterModel(
                    "https://drive.google.com/uc?export=download&id=1RRmRmf5tI3UQ_YKcAD9sdU1H3hKB8qW-",
                    Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create),
                        "pizzas.json"
                    )
                )
            );

            #region Database

            // Create database
            var database = new SQLiteAsyncConnection(
                                Path.Combine(
                                    Environment.GetFolderPath(
                                        Environment.SpecialFolder.LocalApplicationData
                                    ),
                                    "database.db3"
                                )
                            );
            // Create table if not exist
            database.CreateTableAsync<Pizza>().Wait();
            database.CreateTableAsync<UserPreference>().Wait();
            // Inject objet
            unityContainer.RegisterInstance<SQLiteAsyncConnection>(database);

            #endregion Database

            #region AutoMapper

            // Create mapconfig
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                //Create all maps here

                #region Pizza & PizzaModel

                cfg.CreateMap<Pizza, PizzaModel>()
                   .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                   .ForMember(destination => destination.ImageUrl, opts => opts.MapFrom(source => source.ImageUrl))
                   .ForMember(destination => destination.Nom, opts => opts.MapFrom(source => source.Nom))
                   .ForMember(destination => destination.Prix, opts => opts.MapFrom(source => source.Prix))
                   .ForMember(destination => destination.Ingredients, opts => opts.MapFrom(source => !string.IsNullOrEmpty(source.Ingredients) ? source.Ingredients.Split(',').ToList()
                                                                                                                                               : new List<string>()));
                cfg.CreateMap<PizzaModel, Pizza>()
                   .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                   .ForMember(destination => destination.ImageUrl, opts => opts.MapFrom(source => source.ImageUrl))
                   .ForMember(destination => destination.Nom, opts => opts.MapFrom(source => source.Nom))
                   .ForMember(destination => destination.Prix, opts => opts.MapFrom(source => source.Prix))
                   .ForMember(destination => destination.Ingredients, opts => opts.MapFrom(source => source.Ingredients != null ? string.Join(",", source.Ingredients)
                                                                                                                                : string.Empty));

                #endregion Pizza & PizzaModel

                #region UserPreference & UserPreferenceModel

                cfg.CreateMap<UserPreference, UserPreferenceModel>()
                   .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                   .ForMember(destination => destination.Tri, opts => opts.MapFrom(source => (ETri)Enum.Parse(typeof(ETri), source.Tri.ToString())))
                   .ForMember(destination => destination.Favoris, opts => opts.MapFrom(source => !string.IsNullOrEmpty(source.Favoris) ? source.Favoris.Split(',').ToList()
                                                                                                                                       : new List<string>()));
                cfg.CreateMap<UserPreferenceModel, UserPreference>()
                   .ForMember(destination => destination.Id, opts => opts.MapFrom(source => source.Id))
                   .ForMember(destination => destination.Tri, opts => opts.MapFrom(source => (int)source.Tri))
                   .ForMember(destination => destination.Favoris, opts => opts.MapFrom(source => source.Favoris != null ? string.Join(",", source.Favoris)
                                                                                                                        : string.Empty));

                #endregion UserPreference & UserPreferenceModel
            });
            // Inject instance
            unityContainer.RegisterInstance<IMapper>(mapperConfiguration.CreateMapper());

            #endregion AutoMapper

            // Call register types
            unityContainer.RegisterType<IPizzaRepository, PizzaRepositoryV1>();
            unityContainer.RegisterType<IUserPreferenceRepository, UserPreferenceRepository>();
            unityContainer.RegisterType<IPizzaBussiness, PizzaBussiness>();
            unityContainer.RegisterType<IUserPreferenceBussiness, UserPreferenceBussiness>();
            // Set the service locator
            ServiceLocator.SetLocatorProvider(
                () => new UnityServiceLocator(unityContainer)
            );
        }
    }
}