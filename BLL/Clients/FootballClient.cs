using System.Net.Http.Headers;

namespace BLL.Clients
{
    public class FootballClient
    {
        public HttpClient _httpclient;
        public static string _adress;
        public static string _apiKey;

        public FootballClient()
        {
            _adress = "https://api-football-v1.p.rapidapi.com";
            _apiKey = "xxxxxxxxx";

            _httpclient = new HttpClient
            {
                BaseAddress = new Uri(_adress)
            };
            _httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Add("x-rapidapi-key", _apiKey);           
        }        
    }
}
