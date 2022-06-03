using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Newtonsoft.Json; // Nuget Package
using NasaApi.APIIntegration.APIs;
using NasaApi.APIIntegration;

namespace NasaApi
{
    class Program
    {
        static void Main(string[] args)
        {
            var apiCont = new ApiController(new SentryApi(20));
            Console.WriteLine(apiCont.ReadAllData());
            Console.ReadLine();
        }
    }
}
