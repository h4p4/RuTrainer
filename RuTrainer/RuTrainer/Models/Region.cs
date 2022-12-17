namespace RuTrainer.Models
{
    public class Region
    {
        public Settlement[] settlements { get; set; }
        public Codes1 codes { get; set; }
        public string title { get; set; }


        public Country? Country { get; set; }
    }
}