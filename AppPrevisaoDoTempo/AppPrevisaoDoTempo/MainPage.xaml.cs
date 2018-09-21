using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.ComponentModel;

namespace AppPrevisaoDoTempo
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        public MainPage()
        {
            InitializeComponent();
            IsLoading = false;

            //Set the default binding to a default object for now
            BindingContext = this;
        }
        
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.DataBase.GetItemsAsync();
        }

       
        public async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            aparece();
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
            await Task.Delay(3000);
            some();
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
                local.Nome = cidadeEntry.Text.ToUpper();
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

        public async void GetWeatherBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                aparece();
                if (!String.IsNullOrEmpty(cidadeEntry.Text))
                {
                    Tempo tempo = await DataService.GetWeather(cidadeEntry.Text);
                    if (tempo != null)
                    {
                        listView.SelectedItem = false;
                        salvarFavorito.IsEnabled = true;
                        verDetalhes.IsEnabled = true;
                        removerFavorito.IsEnabled = false;
                        BindingContext = tempo;
                    }
                    else
                    {
                        DisplayAlert("Alerta", "Cidade não encontrada!", "OK");
                    }
                }
                await Task.Delay(3000);
                some();
            }
            catch (Exception ex)
            {
                some();
                if (ex != null);
            }
        }

        public async void aparece()
        {
            IsLoading = true;
        }

        public async void some()
        {
            IsLoading = false;
        }

        private bool isLoading;
        public bool IsLoading
        {
            get
            {
                return this.isLoading;
            }
            set
            {
                this.isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
