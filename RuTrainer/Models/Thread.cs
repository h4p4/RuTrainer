using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuTrainer.Models
{
    public class Thread
    {
        public string Number { get; set; }
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public object ExpressType { get; set; }
        public string TransportType { get; set; }
        public Carrier Carrier { get; set; }
        public string Uid { get; set; }
        public object Vehicle { get; set; }
        public TransportSubtype TransportSubtype { get; set; }
        public string ThreadMethodLink { get; set; }
    }
}
