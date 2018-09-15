using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AppPrevisaoDoTempo
{
    class DataService
    {

        //https://docs.microsoft.com/pt-br/visualstudio/cross-platform/learn-app-building-basics-with-xamarin-forms-in-visual-studio
        //https://github.com/xamarin/xamarin-forms-samples/tree/master/Weather
        public static async Task<dynamic> getDataFromService(string queryString)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(queryString);

            dynamic data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject(json);
            }

            return data;
        }

        public static async Task<Tempo> GetWeather(string nomeDaCidade)
        {
            string key = "ac7c243e9a839ca5df46142cca279738";
            string queryString = "http://api.openweathermap.org/data/2.5/weather?q="
                + nomeDaCidade + "&appid=" + key + "&units=metric&lang=pt";

            dynamic results = await DataService.getDataFromService(queryString).ConfigureAwait(false);

            if (results["weather"] != null)
            {
                Tempo tempo = new Tempo();
                tempo.Titulo = (string)results["name"];
                tempo.Temperatura = (string)results["main"]["temp"] + "° C";
                tempo.Vento = (string)results["wind"]["speed"] + " km/h";
                tempo.Humidade = (string)results["main"]["humidity"] + " %";
                tempo.Visibilidade = (string)results["weather"][0]["main"];
                tempo.ImageUrl = "http://openweathermap.org/img/w/" + (string)results["weather"][0]["icon"] + ".png";

                DateTime time = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                DateTime sunrise = time.AddSeconds((double)results["sys"]["sunrise"]);
                DateTime sunset = time.AddSeconds((double)results["sys"]["sunset"]);
                tempo.NascerDoSol = sunrise.ToString() + " UTC";
                tempo.PorDoSol = sunset.ToString() + " UTC";
                return tempo;
            }
            else
            {
                return null;
            }
        }
    }
}
