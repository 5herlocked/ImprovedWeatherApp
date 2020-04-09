using System;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class Core
    {
        public static async Task<Weather> GetWeather(string zipCode)
        {
            //Sign up for a free API key at http://openweathermap.org/appid
            string key = "Insert_api_key";
            string queryString = "https://api.openweathermap.org/data/2.5/weather?zip="
                + zipCode + ",us&appid=" + key + "&units=imperial";

            dynamic results = await DataService.getDataFromService(queryString).ConfigureAwait(false);

            if (results["weather"] != null)
            {
                DateTime time = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                DateTime sunrise = time.AddSeconds((double)results["sys"]["sunrise"]);
                DateTime sunset = time.AddSeconds((double)results["sys"]["sunset"]);

                Weather weather = new Weather
                {
                    Title = (string)results["name"],
                    Temperature = (string)results["main"]["temp"] + " F",
                    Wind = (string)results["wind"]["speed"] + " mph",
                    Humidity = (string)results["main"]["humidity"] + " %",
                    Visibility = (string)results["weather"][0]["main"],
                    Sunrise = sunrise.ToString() + " UTC",
                    Sunset = sunset.ToString() + " UTC",
                };
                return weather;
            }

            return null;
        }
    }
}