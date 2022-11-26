namespace RuTrainer.Models
{
    public class Settlement
    {
        public string title { get; set; }
        public Codes2 codes { get; set; }
        public Station[] stations { get; set; }
    }
}