using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Newtonsoft.Json; // Nuget Package

namespace NasaApi.APIIntegration.APIs
{
    public class DailyImageApi : IApiController
    {
        private string url;
        private Dictionary<string, string> data;

        public DailyImageApi()
        {
            this.url = "https://api.nasa.gov/planetary/apod?api_key=wydPDqHhNGFERM9UgVikTVFT83195ObRVjhxMmOv";
            this.data = this.GetAllData();
        }

        private Dictionary<string, string> GetAllData()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(this.url).Result;
            if (response.IsSuccessStatusCode)
            {
                Dictionary<string, string> imageData = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ReadAsStringAsync().Result);
                return imageData;
            }
            return null;
        }

        public string ReadAllData()
        {
            string allData = "";
            foreach(KeyValuePair<string, string> row in this.data)
            {
                allData += $"{row.Key}: {row.Value}\n";
            }
            return allData;
        }

        public string GetDataByParam(string param)
        {
            return this.data[param];
        }
    }
}
