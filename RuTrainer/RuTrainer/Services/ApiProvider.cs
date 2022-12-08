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
        static HttpClient httpClient = new HttpClient();
        static List<Station> Stations { get; set; }
        public static async Task<Root> GetTripsByStationAsync(string station)
        {
            string request = $"https://api.rasp.yandex.net/v3.0/schedule/?" +
                $"apikey=d2054f31-2c2b-4544-8a59-f8f604853ba5&" +
                $"transport_types=train&" +
                $"direction=на%20Москву&" +
                $"date=2022-11-28";
            HttpResponseMessage response =
                (await httpClient.GetAsync(request)).EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            JObject jObject = JObject.Parse(responseBody);
            Root root = jObject.ToObject<Root>();

            return root;
        }

        public static async Task<Station[]> GetNearbyStationsAsync()
        {
            HttpResponseMessage response =
                (await httpClient.GetAsync($"https://api.rasp.yandex.net/v3.0/nearest_stations/?" +
                $"apikey=d2054f31-2c2b-4544-8a59-f8f604853ba5&" +
                $"format=json&" +
                $"transport_types=train&" +
                $"lat=56.440046&" +
                $"lng=84.4882367&" +
                $"distance=40&" +
                $"lang=ru_RU")).EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            JObject jObject = JObject.Parse(responseBody);
            Rootobject root = jObject.ToObject<Rootobject>();

            return root.stations;
        }

        public static async Task InitStationsAsync()
        {
            HttpResponseMessage response =
                (await httpClient.GetAsync($"https://api.rasp.yandex.net/v3.0/stations_list/?" +
                $"apikey=d2054f31-2c2b-4544-8a59-f8f604853ba5&" +
                $"format=json&" +
                $"transport_types=train&" +
                $"lang=ru_RU")).EnsureSuccessStatusCode();
            var rg = JObject.Parse(await response.Content.ReadAsStringAsync())
                          .ToObject<Rootobject>().countries
                          .Where(x => x.title == "Россия").First().regions;
            Stations = new List<Station>();
            foreach (var r in rg)
                foreach (var s in r.settlements)
                    foreach (var st in s.stations.Where(x => x.transport_type == "train" || x.transport_type == "поезд"))
                    {
                        st.Settlement = s;
                        Stations.Add(st);
                    }
        }

    }
}
