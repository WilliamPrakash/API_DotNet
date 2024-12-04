using System.Runtime.InteropServices;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace api.DAL
{
	public class Authenticate
	{
		public string pwd = "";

        public Dictionary<string,string>? OpenAuthFile()
		{
			Console.WriteLine("authenticate");
            string path = "";
            // Path depends on which computer I'm working on
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			{
				path = @"/Users/williamprakash/Desktop/Credentials.json";
			}
			else if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
                path = "C:/Users/willi/Downloads/credentials.json";
            }
			if (File.Exists(path))
			{
				string jsonToParse = File.ReadAllText(path);
				Dictionary<string,string>? dict = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonToParse);

				// Grab password from local file
				if (dict.Count != 0)
				{
                    for (int i = 0; i < dict.Keys.Count; i++)
                    {
                        if (dict.ElementAt(i).Key == "MongoDB")
                        {
							pwd = dict.ElementAt(i).Value;
                        }
                    }
                }

				// Configure Mongo connection string with password
				var mongoConnStr = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings:MongoDB").Value;
				var mongoConnStrPwd = mongoConnStr.Replace("<password>", pwd);
				DBConnect x = new DBConnect();
				x.GetMongoDBInstance(mongoConnStrPwd);

                return dict;
			}
			Dictionary<string, string>? empty = new Dictionary<string, string>();
			return empty;
		}
	}
}

