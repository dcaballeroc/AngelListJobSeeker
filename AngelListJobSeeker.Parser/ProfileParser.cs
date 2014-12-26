using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace AngelListJobSeeker.Parser
{
    public class ProfileParser
    {
        public static dynamic ParseFile(string fileName)
        {
            var fileContent = GetFileContent(fileName);
            dynamic deserializedContent;

            try
            {
                deserializedContent = JsonConvert.DeserializeObject(fileContent);
            }
            catch (Exception)
            {
                throw new Exception("The JSON content in format is not formatted correctly");
            }

            return deserializedContent;
        }

        private static string GetFileContent(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException();
            }

            return File.ReadAllText(fileName);
        }
    }
}
