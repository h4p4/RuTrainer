using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuTrainer.Models
{
    public class Carrier
    {
        public int Code { get; set; }
        public string Title { get; set; }
        public Codes Codes { get; set; }
        public object Address { get; set; }
        public string Url { get; set; }
        public object Email { get; set; }
        public string Contacts { get; set; }
        public string Phone { get; set; }
        public object Logo { get; set; }
        public object LogoSvg { get; set; }
    }
}
