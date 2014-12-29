using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AngelListJobSeeker.Engine
{
    internal class ApiWrapper
    {
        public static string GetJobListing()
        {
            var jobs = new JArray();
            var apiUrl = ConfigurationManager.AppSettings["AngelListApiUrl"];
            var numberOfPages = Convert.ToInt32(ConfigurationManager.AppSettings["MaxJobsPagesToSeek"]);

            for (var page = 1; page <= numberOfPages; page++)
            {
                var request = (HttpWebRequest) WebRequest.Create(string.Format(apiUrl, page));
                var response = (HttpWebResponse) request.GetResponse();

                if(response.StatusCode != HttpStatusCode.OK)
                    throw new Exception("Couldn't connect to AngelList API");

                var dataStream = response.GetResponseStream();
                var streamReader = new StreamReader(dataStream);
                var responseContent = streamReader.ReadToEnd();
                var deserializedResponseContent = JObject.Parse(responseContent);

                streamReader.Close();
                response.Close();
                jobs = new JArray(jobs.Union(deserializedResponseContent["jobs"]));
            }

            return JsonConvert.SerializeObject(jobs);
        }
    }
}
