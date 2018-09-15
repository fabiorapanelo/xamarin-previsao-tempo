using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppPrevisaoDoTempo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //Set the default binding to a default object for now
            BindingContext = new Tempo();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.DataBase.GetItemsAsync();
        }

        private async void GetWeatherBtn_Clicked(object sender, EventArgs e)
        {
            
            if (!String.IsNullOrEmpty(cidadeEntry.Text))
            {
                Tempo tempo = await DataService.GetWeather(cidadeEntry.Text);
                if (tempo != null)
                {
                    salvarFavorito.IsEnabled = true;
                    verDetalhes.IsEnabled = true;
                    removerFavorito.IsEnabled = false;
                    BindingContext = tempo;
                }
            }
        }

        public async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var local = e.SelectedItem as Local;
            if (local != null)
            {
                Tempo tempo = await DataService.GetWeather(local.Nome);
                if (tempo != null)
                {
                    salvarFavorito.IsEnabled = true;
                    verDetalhes.IsEnabled = true;
                    BindingContext = tempo;
                    tempo.Local = local;
                    removerFavorito.IsEnabled = true;
                }
            }
        }

        private async void verDetalhes_Clicked(object send, EventArgs e)
        {
            await Navigation.PushAsync(new VerDetalhes
            {
                BindingContext = BindingContext
            });
        }

        private async void salvarFavorito_Clicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cidadeEntry.Text))
            {
                Local local = new Local();
                local.Nome = cidadeEntry.Text;
                await App.DataBase.SaveItemAsync(local);
                listView.ItemsSource = await App.DataBase.GetItemsAsync();
            }

        }

        private async void removerFavorito_Clicked(object sender, EventArgs e)
        {
            Tempo tempo = BindingContext as Tempo;
            if(tempo.Local != null)
            {
                await App.DataBase.DeleteItemAsync(tempo.Local);
                listView.ItemsSource = await App.DataBase.GetItemsAsync();
                removerFavorito.IsEnabled = false;
            }
        }
    }
}
