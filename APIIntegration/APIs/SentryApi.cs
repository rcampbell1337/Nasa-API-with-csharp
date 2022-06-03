using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Newtonsoft.Json; // Nuget Package
using NasaApi.APIIntegration;
using Newtonsoft.Json.Linq;

namespace NasaApi.APIIntegration.APIs
{
    class Sentry
    {
        private readonly int id;
        private string ip, range, diameter, last_obs, fullname, ps_cum;
        
        public Sentry(int id, dynamic data)
        {
            this.id = id;
            this.ConvertData(data);
        }

        public void ConvertData(dynamic data)
        {
            this.ip = data["ip"];
            this.range = data["range"];
            this.diameter = data["diameter"];
            this.last_obs = data["last_obs"];
            this.fullname = data["fullname"];
            this.ps_cum = data["ps_cum"];
        }

        public string GetSentryData()
        {
            return $"Full Name of Object: {this.fullname}\n" +
                $"Probability of Impact: {this.ip}\n" +
                $"Range from Earth: {this.range}\n" +
                $"Diameter in KM: {this.diameter}\n" +
                $"Time of Last Observation: {this.last_obs}\n" +
                $"The Cumulitive Hazard Rating: {this.ps_cum}\n";
        }
    }
    class SentryApi : IApiController
    {
        private readonly string url = "https://ssd-api.jpl.nasa.gov/sentry.api?days=";
        private List<Sentry> sentryList;
        private readonly int spanOfDays;

        public SentryApi(int spanOfDays)
        {
            this.spanOfDays = spanOfDays;
            this.url += this.spanOfDays;
            this.GetAllData();
        }

        public void GetAllData()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(this.url).Result;
            Dictionary<dynamic, dynamic> imageData = null;
            if (response.IsSuccessStatusCode)
            {
                imageData = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(response.Content.ReadAsStringAsync().Result);
            }
            this.ConvertDataIntoObjects(imageData);
        }

        public void ConvertDataIntoObjects(Dictionary<dynamic, dynamic> data)
        {
            int index = 1;
            this.sentryList = new List<Sentry>();
            foreach(var sentryData in data["data"])
            {
                this.sentryList.Add(new Sentry(index, sentryData));
                index++;
            }
        }

        public string ReadAllData()
        {
            string results = $"Results for the last {this.spanOfDays} days.\n\n";
            foreach(Sentry sentry in this.sentryList)
            {
                results += $"{sentry.GetSentryData()}\n";
            }
            return results;
        }
    }
}
