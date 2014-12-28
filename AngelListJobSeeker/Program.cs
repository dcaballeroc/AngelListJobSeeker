using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AngelListJobSeeker.Engine;
using AngelListJobSeeker.Parser;

namespace AngelListJobSeeker.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = ProfileParser.ParseFile("test.json");
            Console.WriteLine(obj.year_format);
            JobMatcher.GetTopTenJobs(obj);

            Console.ReadKey();
        }
    }
}
