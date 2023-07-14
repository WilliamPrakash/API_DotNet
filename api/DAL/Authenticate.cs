using System.Text.Json;

namespace api.DAL
{
	public class Authenticate
	{
		// do anything in the constructor?

		// currently grabbing all key-value pairs in credentials.json
		public Dictionary<string,string>? OpenAuthFile()
		{
			// TODO: add check for OS type, filepath will change
			Console.WriteLine("authenticate");
			string path = @"/Users/williamprakash/Desktop/credentials.json";
			if (File.Exists(path))
			{
				string jsonToParse = File.ReadAllText(path);
				Dictionary<string,string>? dict = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonToParse);
				Console.WriteLine(dict);

				

				return dict;
			}
			Dictionary<string, string>? empty = new Dictionary<string, string>();
			return empty;
		}
	}
}

