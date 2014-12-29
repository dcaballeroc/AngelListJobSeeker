using System;
using System.IO;

using Newtonsoft.Json;

using AngelListJobSeeker.Engine.Models;

namespace AngelListJobSeeker.Parser
{
    public class ProfileParser
    {
        public static Profile ParseFile(string fileName)
        {
            var fileContent = GetFileContent(fileName);
            Profile profile;

            try
            {
                profile = JsonConvert.DeserializeObject<Profile>(fileContent);
            }
            catch (Exception ex)
            {
                throw new Exception("The JSON content in format is not formatted correctly");
            }

            return profile;
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
