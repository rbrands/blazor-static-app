using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Components.Forms;

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
        public async Task<ClientPrincipal> GetUserDetails()
        {
            return await _http.GetFromJsonAsync<ClientPrincipal>($"/api/GetUserDetails");
        }

        public async Task<BlobAccessSignature> GetBlobAccessSignatureForPNGImageUpload()
        {
            return await _http.GetFromJsonAsync<BlobAccessSignature>($"/api/GetBlobAccessSignatureForPNGImageUpload");
        }

        public async Task<BlobAccessSignature> UploadImage(IBrowserFile picture)
        {
            BlobAccessSignature sas = await GetBlobAccessSignatureForPNGImageUpload();
            BlobClient blobClient = new BlobClient(sas.Sas);

            using var stream = picture.OpenReadStream(maxAllowedSize: 2048000);
            var blobHttpHeader = new BlobHttpHeaders();
            blobHttpHeader.ContentType = "image/png";
            await blobClient.UploadAsync(stream, blobHttpHeader);

            return sas;
        }

    }
}
