using PizzaApp.Domain.Models;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace PizzaApp.ViewModels
{
    public class PizzaCellViewModel : PizzaCellModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand FavoriteClickCommand { get; set; }
        public Action<PizzaCellViewModel> FavoriteChangeAction { get; set; }

        public PizzaCellViewModel(PizzaModel pizza, Action<PizzaCellViewModel> onFavoriteChangeAction)
        {
            this.Pizza = pizza;
            this.FavoriteChangeAction = onFavoriteChangeAction;
            this.FavoriteClickCommand = new Command(
                                            (obj) =>
                                            {
                                                this.IsFavorite = !this.IsFavorite;
                                                this.OnPropertyChanged("ImageSource");
                                                this.FavoriteChangeAction.Invoke(this);
                                            }
                                        );
        }

        protected void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}