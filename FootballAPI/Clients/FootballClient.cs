using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FootballAPI.Models;
using Newtonsoft.Json;

namespace FootballAPI.Clients
{
    public class FootballClient
    {
        public HttpClient _httpclient;
        public static string _adress;
        public static string _apiKey;

        public FootballClient()
        {
            _adress = Constants.adress;
            _apiKey = Constants.apiKey;

            _httpclient = new HttpClient();
            _httpclient.BaseAddress = new Uri(_adress);
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Add("x-rapidapi-key", _apiKey);
           
        }
        
    }
}
