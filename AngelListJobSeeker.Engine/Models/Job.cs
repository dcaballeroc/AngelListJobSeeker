using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngelListJobSeeker.Engine.Models
{
    public class Job
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string angellist_url { get; set; }
        public double? equity_min { get; set; }
        public double? equity_max { get; set; }
        public string job_type { get; set; }
        public string currency_code { get; set; }
        public double? salary_min { get; set; }
        public double? salary_max { get; set; }
        public List<Tag> tags { get; set; }
        public Startup startup { get; set; }
        public int job_rank { get; set; }
    }
}
