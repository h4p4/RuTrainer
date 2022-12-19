using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using RuTrainer.Models;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RuTrainer.Services
{
    public static class ApiProvider
    {
        private static HttpClient httpClient = new HttpClient();

        public static List<Station> AllStations = new List<Station>();
        public static List<Settlement> AllSettlements = new List<Settlement>();       
        public static Rootobject MainRoot;
        public static async Task LoadStationsAsync()
        {
            HttpResponseMessage response =
                (await httpClient.GetAsync($"https://api.rasp.yandex.net/v3.0/stations_list/?" +
                $"apikey=d2054f31-2c2b-4544-8a59-f8f604853ba5&" +
                $"format=json&" +
                $"transport_types=train&" +
                $"lang=ru_RU")).EnsureSuccessStatusCode();
            MainRoot = JObject.Parse(await response.Content.ReadAsStringAsync())
                          .ToObject<Rootobject>();
            var country = MainRoot.countries.Where(x => x.title == "Россия").First();
            foreach (var r in country.regions)
            {
                r.Country = country; 
                foreach (var s in r.settlements/*.Where(x => !String.IsNullOrWhiteSpace(x.title))*/)
                {
                    s.Region = r;
                    AllSettlements.Add(s);
                    foreach (var st in s.stations.Where(x => x.transport_type == "train" || x.transport_type == "поезд"))
                    {
                        st.Settlement = s;
                        AllStations.Add(st);
                    }
                }
            }

        }
        public static async Task<Root> GetTripsByStationsAsync(string from, string to)
        {
            var date = (DateTime.Now.AddDays(1)).ToString("yyyy-MM-dd");
            string request = $"https://api.rasp.yandex.net/v3.0/search/?" +
                $"apikey=d2054f31-2c2b-4544-8a59-f8f604853ba5&" +
                $"transport_types=train&" +
                $"from={from}&" +
                $"to={to}&" +
                $"date={date}";
            HttpResponseMessage response =
                (await httpClient.GetAsync(request)).EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            JObject jObject = JObject.Parse(responseBody);
            Root root = jObject.ToObject<Root>();
            return root;
        }


    }
}
