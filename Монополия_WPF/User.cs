using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly_WPF
{
   public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Rank { get; set; }
        public Uri ImgUrl { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public string StatusInSystem { get; set; }
    }
}
