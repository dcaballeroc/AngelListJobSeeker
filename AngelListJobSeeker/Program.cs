using System;
using System.Linq;

using AngelListJobSeeker.Engine;
using AngelListJobSeeker.Engine.Models;
using AngelListJobSeeker.Parser;

namespace AngelListJobSeeker.App
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = Environment.CurrentDirectory + "\\..\\..\\";
            var companyEnumerator = 1;
            var profile = ProfileParser.ParseFile(str + "test.json");
            var topTenCompanies = JobMatcher.GetTopTenCompanies(profile);

            Console.WriteLine("Hi {0}, these are the companies that matches your profile:", profile.name);
            Console.WriteLine("\n");

            foreach (IGrouping<int, Job> company in topTenCompanies)
            {
                var companyInfo = company.First().startup;
                Console.WriteLine("{0}. {1}{2}\n", companyEnumerator, companyInfo.name,
                                  string.IsNullOrEmpty(companyInfo.company_url)
                                      ? string.Empty
                                      : " (" + companyInfo.company_url + ")");
                Console.WriteLine("{0}\n",
                                  string.IsNullOrEmpty(companyInfo.product_desc)
                                      ? "<Company description not provided>"
                                      : companyInfo.product_desc);
                Console.WriteLine("Current openings:\n");

                var jobEnumerator = 1;

                foreach (var job in company)
                {
                    Console.WriteLine("  {0}. {1}\n  Description: {2}\n", jobEnumerator, job.title, job.description);
                    Console.WriteLine("  More job details: {0}\n", job.angellist_url);

                    ++jobEnumerator;
                }

                Console.WriteLine("Press any key to see next company...");
                Console.ReadKey();
                ++companyEnumerator;
            }

            Console.Write("Press any key to finish...");
            Console.ReadKey();
        }
    }
}
