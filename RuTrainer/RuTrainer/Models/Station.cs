using static Chuck.ApiProvider;

namespace RuTrainer.Models
{
    public class Station
    {
        public string? direction { get; set; }
        public Codes3? codes { get; set; }
        public string? station_type { get; set; }
        public string? title { get; set; }
        public object? longitude { get; set; }
        public string? transport_type { get; set; }
        public object? latitude { get; set; }
        public string? type { get; set; }
        public string? short_title { get; set; }
        public string? popular_title { get; set; }
        public string? code { get; set; }
        public float? lat { get; set; }
        public float? lng { get; set; }
        public string? station_type_name { get; set; }
        public float? distance { get; set; }
        public int? majority { get; set; }
    }
}