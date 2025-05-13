using System.Text.Json;
using System.Runtime.InteropServices;


namespace API.DAL;

public class DatabaseConnect
{
    Dictionary<string, string>? localCredentials;
    public string sqlConnStr = "";
    private string sqlConnStr_Win = "";
    private string sqlConnStr_Mac = "";

    public DatabaseConnect()
    {
        localCredentials = OpenLocalAuthFile();

        // If no DB credentials are found, shut down the application
        if (localCredentials == null || localCredentials.Count == 0) { System.Environment.Exit(1); }

        for (int i = 0; i < localCredentials.Keys.Count; i++)
        {
            switch (localCredentials.ElementAt(i).Key)
            {
                case "SQLServer_Win":
                    sqlConnStr_Win = localCredentials.ElementAt(i).Value;
                    break;
                case "SQLServer_Mac":
                    sqlConnStr_Mac = localCredentials.ElementAt(i).Value;
                    break;
            }
        }

        // Set main SQL connection string based on the host OS
        sqlConnStr = sqlConnStr_Win;
        if (System.Environment.OSVersion.Platform == PlatformID.Unix)
        {
            sqlConnStr = sqlConnStr_Mac;
        }
        Console.WriteLine("Main SQL connection string set");
    }

    private Dictionary<string, string> OpenLocalAuthFile()
    {
        Console.WriteLine("Attempting to grab DB credentials from local credentials.json...");

        // Path depends on which computer I'm developing on
        string path = "C:/Users/willi/Desktop/credentials.json";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            path = @"/Users/williamprakash/Desktop/credentials.json";
        }

        // Attempt to grab credentials
        if (File.Exists(path))
        {
            string jsonToParse = File.ReadAllText(path);
            Dictionary<string, string>? dict = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonToParse);

            // If nothing was found in credentials.json, return an empty dictionary
            if (dict == null)
            {
                Console.WriteLine(path + " contains no credentials.");
                return new Dictionary<string, string> { { "", "" } };
            }
            ;

            Console.WriteLine("DB credentials grabbed.");
            return dict;
        }
        else // Path to credentials.json not found
        {
            Console.WriteLine(path + " not found.");
            return new Dictionary<string, string>();
        }
    }

}
