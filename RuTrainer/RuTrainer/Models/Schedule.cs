using System;
using System.Collections.Generic;
using System.Text;

namespace RuTrainer.Models
{
    public class Schedule
    {
        public Thread thread { get; set; }
        public bool is_fuzzy { get; set; }
        public string platform { get; set; }
        public object terminal { get; set; }
        public string days { get; set; }
        public object except_days { get; set; }
        public string stops { get; set; }
        public DateTime departure { get; set; }
        public object arrival { get; set; }
    }
}
