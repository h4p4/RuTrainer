using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuTrainer.Models
{
    public class Root
    {
        public Search Search { get; set; }
        public List<Segment> Segments { get; set; }
        public List<object> IntervalSegments { get; set; }
        public Pagination Pagination { get; set; }
    }

}
