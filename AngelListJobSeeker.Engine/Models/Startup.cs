using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngelListJobSeeker.Engine.Models
{
    public class Startup
    {
        public int id { get; set; }
        public string name { get; set; }
        public string company_url { get; set; }
        public string product_desc { get; set; }
        public bool community_profile { get; set; }
        public int quality { get; set; }
        public int rank { get; set; }
    }
}
