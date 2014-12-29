using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngelListJobSeeker.Engine.Models
{
    public class Profile
    {
        public string name { get; set; }
        public string bio { get; set; }
        public string image { get; set; }
        public string milestones { get; set; }
        public object home_address { get; set; }
        public bool willing_to_relocate { get; set; }
        public List<string> looking_for { get; set; }
        public List<object> experience { get; set; }
        public List<object> projects { get; set; }
        public List<string> skills { get; set; } 
    }
}
