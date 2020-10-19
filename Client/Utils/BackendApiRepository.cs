using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using BlazorApp.Shared;


namespace BlazorApp.Client.Utils
{
    public class BackendApiRepository
    {
        HttpClient _http;

        public BackendApiRepository(HttpClient http)
        {
            _http = http;
        }

        public async Task<WeatherForecast[]> FetchWeatherForecast()
        {
            return await _http.GetFromJsonAsync<WeatherForecast[]>("/api/WeatherForecast");
        }
        public async Task<string> GetConfig(string name)
        {
            return await _http.GetStringAsync($"/api/GetConfigValue?name={name}");
        }

    }
}
