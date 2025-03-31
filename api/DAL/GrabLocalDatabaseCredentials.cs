using System.Runtime.InteropServices;
using System.Text.Json;

namespace api.DAL
{
	public class GrabLocalDatabaseCredentials
	{
		public string mongoPwd = "";
        public string sqlConnStr = "";

        public Dictionary<string,string>? OpenLocalAuthFile()
		{
			Console.WriteLine("Attempting to grab DB credentials from local credentials.json...");
            string path = "";

            // Path depends on which computer I'm developing on
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			{
				path = @"/Users/williamprakash/Desktop/credentials.json";
			}
			else if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
                path = "C:/Users/willi/Desktop/credentials.json";
            }

			// Grab credentials
			if (File.Exists(path))
			{
				string jsonToParse = File.ReadAllText(path);
				Dictionary<string,string>? dict = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonToParse);

				// If nothing was found in credentials.json, return an empty dictionary
				if (dict == null) { return new Dictionary<string, string> { { "", "" } }; };

				Console.WriteLine("DB credentials grabbed.");
                return dict;
			}
            else // Path to credentials.json not found
            {
				Console.WriteLine("DB credentials not found.");
                Dictionary<string, string>? empty = new Dictionary<string, string>();
                return empty;
            }
		}
	}
}

