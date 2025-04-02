using MongoDB.Driver;
using api.Models.MongoDB;


namespace api.DAL
{
	public class DatabaseConnect
	{
        Dictionary<string, string>? localCredentials;
        public static string sqlConnStr = "";
        private static string sqlConnStr_Win = "";
        private static string sqlConnStr_Mac = "";
        private static string mongoPwd = "";
        public IMongoCollection<Client>? _clientCollection;

        // Only one instance gets created in Program.cs
        public DatabaseConnect()
        {
            GrabLocalDatabaseCredentials creds = new GrabLocalDatabaseCredentials();
            localCredentials = creds.OpenLocalAuthFile();

            // If no DB credentials are found, shut down the application
            if (localCredentials == null || localCredentials.Count == 0) { System.Environment.Exit(1); }

            for (int i = 0; i < localCredentials.Keys.Count; i++)
            {
                switch (localCredentials.ElementAt(i).Key)
                {
                    case "MongoDB":
                        mongoPwd = localCredentials.ElementAt(i).Value;
                        break;
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
            Console.WriteLine("SQL connection string set");
            //Console.WriteLine("DB credentials parsed/established/whatever...");
        }
	}
}

