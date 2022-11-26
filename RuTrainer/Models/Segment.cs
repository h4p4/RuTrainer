using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuTrainer.Models
{
    public class Segment
    {
        public Thread Thread { get; set; }
        public string Stops { get; set; }
        public From From { get; set; }
        public To To { get; set; }
        public string DeparturePlatform { get; set; }
        public string ArrivalPlatform { get; set; }
        public object DepartureTerminal { get; set; }
        public object ArrivalTerminal { get; set; }
        public double Duration { get; set; }
        public bool HasTransfers { get; set; }
        public TicketsInfo TicketsInfo { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public string StartDate { get; set; }
    }
}
