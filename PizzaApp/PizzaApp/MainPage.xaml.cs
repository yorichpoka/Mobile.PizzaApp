using Microsoft.Practices.ServiceLocation;
using PizzaApp.Bussiness;
using PizzaApp.Domain;
using PizzaApp.Domain.Interfaces;
using PizzaApp.Domain.Models;
using PizzaApp.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PizzaApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private UserPreferenceModel _UserPreference { get; set; }
        private InputParameterModel _InputParameter { get; set; }
        private readonly IPizzaBussiness _PizzaBussiness;
        private readonly IUserPreferenceBussiness _UserPreferenceBussiness;

        public MainPage()
        {
            InitializeComponent();

            // Get bussiness service form locator
            this._PizzaBussiness = ServiceLocator.Current.GetInstance<PizzaBussiness>();
            this._UserPreferenceBussiness = ServiceLocator.Current.GetInstance<UserPreferenceBussiness>();
            this._InputParameter = ServiceLocator.Current.GetInstance<InputParameterModel>();
            // Load user preference
            this._UserPreference = this._UserPreferenceBussiness.Get().Result;

            // Define event
            DefineEvents();
            // Load items source
            LoadItemsSourceListView();
            // Load session data
            LoadImgButtonSource();
        }

        /// <summary>
        /// Load items source
        /// </summary>
        public async Task LoadItemsSourceListView()
        {
            try
            {
                var result = await this._PizzaBussiness.GetFromTri(
                                this._UserPreference.Tri,
                                this._InputParameter.UrlLocalStorageFile,
                                this._UserPreference.Favoris
                            );

                this.LstView.ItemsSource = result.Value.ExtConvertTo(this._UserPreference.Favoris, OnFavoriteChangeAction);

                if (result.Key == false)
                    throw new Exception();
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(
                    () => {
                        DisplayAlert("Erreur", $"An error occured: {ex.Message}!", "OK");
                    }
                );
            }
            finally
            {
                this.LstView.IsRefreshing = false;
                this.LstView.IsVisible = true;
                this.stklyt.IsVisible = false;
            }
        }

        /// <summary>
        /// Load events
        /// </summary>
        private void DefineEvents()
        {
            this.LstView.RefreshCommand =
                new Command(
                    async (obj) =>
                    {
                        this.LstView.IsRefreshing = true;
                        await LoadItemsSourceListView();
                        this.LstView.IsRefreshing = false;
                    }
                );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortImgButton_Clicked(object sender, EventArgs e)
        {
            // Update variable
            this._UserPreference.SetTri();
            // Save parameter
            this._UserPreferenceBussiness.Set(this._UserPreference);
            // Update iage of button
            this.SortImgButton.Source = this._UserPreference.Tri.ExtGetImageSource();
            // Update data source
            LoadItemsSourceListView();
        }

        /// <summary>
        ///
        /// </summary>
        private void LoadImgButtonSource()
        {
            // Update iage of button
            this.SortImgButton.Source = this._UserPreference.Tri.ExtGetImageSource();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pizzaCell"></param>
        private void OnFavoriteChangeAction(PizzaCellViewModel pizzaCell)
        {
            // Update favoris list
            this._UserPreference.SetFavoris(pizzaCell);
            // Save in database
            this._UserPreferenceBussiness.Set(this._UserPreference);
        }
    }
}