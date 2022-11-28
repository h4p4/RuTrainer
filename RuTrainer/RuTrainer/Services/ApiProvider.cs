using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using RuTrainer.Models;
using System.Text;
using System.Threading.Tasks;

namespace RuTrainer.Services
{
    public static class ApiProvider
    {
        static HttpClient httpClient = new HttpClient();
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


    }
}
