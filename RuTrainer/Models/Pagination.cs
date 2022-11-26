using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuTrainer.Models
{
    public class Pagination
    {
        public int Total { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}
