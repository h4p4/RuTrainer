namespace RuTrainer.Models
{
    public class Rootobject
    {
        public Country[]? countries { get; set; }


        public string date { get; set; }
        public Station station { get; set; }
        public string _event { get; set; }
        public Pagination pagination { get; set; }
        public Schedule[] schedule { get; set; }
        public object[] interval_schedule { get; set; }
    }
}