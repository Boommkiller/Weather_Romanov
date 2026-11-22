using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather.Classes
{
    public static class WeatherApi
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<List<WeatherRow>> GetForecast(double latitude, double longitude)
        {
            string url = "https://api.open-meteo.com/v1/forecast" +
                "?latitude=" + latitude.ToString(CultureInfo.InvariantCulture) +
                "&longitude=" + longitude.ToString(CultureInfo.InvariantCulture) +
                "&hourly=temperature_2m,apparent_temperature,relative_humidity_2m,surface_pressure,wind_speed_10m,weather_code" +
                "&forecast_days=3&timezone=auto&wind_speed_unit=ms";
            string json = await client.GetStringAsync(url);
            Forecast forecast = JsonConvert.DeserializeObject<Forecast>(json);

            List<WeatherRow> rows = new List<WeatherRow>();
            for (int i = 0; i < forecast.Hourly.Time.Count; i += 3)
            {
                rows.Add(new WeatherRow()
                {
                    DateTime = DateTime.Parse(forecast.Hourly.Time[i], CultureInfo.InvariantCulture),
                    Temperature = FormatTemperature(forecast.Hourly.Temperature[i]),
                    FeelsLike = FormatTemperature(forecast.Hourly.ApparentTemperature[i]),
                    Pressure = (int)Math.Round((forecast.Hourly.Pressure[i] ?? 0) * 0.750062),
                    Humidity = (forecast.Hourly.Humidity[i] ?? 0) + "%",
                    Wind = (forecast.Hourly.WindSpeed[i] ?? 0).ToString("0.0") + " м/с",
                    Description = GetDescription(forecast.Hourly.WeatherCode[i] ?? 0)
                });
            }
            return rows;
        }

        private static string FormatTemperature(double? value)
        {
            int t = (int)Math.Round(value ?? 0);
            return (t > 0 ? "+" : "") + t + "°";
        }

        private static string GetDescription(int code)
        {
            switch (code)
            {
                case 0: return "Ясно";
                case 1: return "Преимущественно ясно";
                case 2: return "Облачно с прояснениями";
                case 3: return "Пасмурно";
                case 45:
                case 48: return "Туман";
                case 51:
                case 53:
                case 55: return "Морось";
                case 56:
                case 57: return "Ледяная морось";
                case 61: return "Небольшой дождь";
                case 63: return "Дождь";
                case 65: return "Сильный дождь";
                case 66:
                case 67: return "Ледяной дождь";
                case 71: return "Небольшой снег";
                case 73: return "Снег";
                case 75: return "Сильный снег";
                case 77: return "Снежная крупа";
                case 80:
                case 81:
                case 82: return "Ливень";
                case 85:
                case 86: return "Снегопад";
                case 95: return "Гроза";
                case 96:
                case 99: return "Гроза с градом";
                default: return "Нет данных";
            }
        }
    }
}
