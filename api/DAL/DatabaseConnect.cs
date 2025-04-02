using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.Data.SqlClient;
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

        // Constructor
        public DatabaseConnect()
        {
            GrabLocalDatabaseCredentials creds = new GrabLocalDatabaseCredentials();
            localCredentials = creds.OpenLocalAuthFile();

            // If no DB credentials are found, shut down the application.
            if (localCredentials == null) { System.Environment.Exit(1); }

            if (localCredentials.Count != 0)
            {
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
            }

            // MongoDB ***NOT WORKING
            //if (mongoPwd != "")
            /*if (false)
            {
                var mongoConnStr = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings:MongoDB").Value;
                mongoConnStr = mongoConnStr.Replace("<db_password>", mongoPwd);
                MongoDBConnect(mongoConnStr);
            }*/

            // Check OS
            sqlConnStr = sqlConnStr_Win;
            if (System.Environment.OSVersion.Platform == PlatformID.Unix)
            {
                sqlConnStr = sqlConnStr_Mac;
            }

            // SQL Server (Local)
            using (SqlConnection connection = new SqlConnection(sqlConnStr))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("select * from master.dbo.Employees", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["id"] + " " + reader["Name"] + " " + reader["Email"] + " " + reader["Occupation"]);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }

        }

        /* NOT WORKING */
        public void MongoDBConnect(string mongoConnStr)
		{
            // Create Client, connect to DB, connect to Collection
            var settings = MongoClientSettings.FromConnectionString(mongoConnStr);
            //settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            MongoClient client = new MongoClient(mongoConnStr);
            
            var dbList = client.ListDatabases().ToList();
            //client.Settings.Credential = new MongoCredential( "willprakash";
			IMongoDatabase db = client.GetDatabase("MainDB");
            db.RunCommand<BsonDocument>(new BsonDocument("ping",1));
            _clientCollection = db.GetCollection<Client>("Clients");

			var test = _clientCollection.Find(x => x.name == "Will").ToList();

            Console.WriteLine(test);
		}

		// This will be called from any endpoint that wants to query the SQL DB
		public static SqlConnection SQLServerConnect(string sqlConnStr)
		{
			SqlConnection connection = new SqlConnection(sqlConnStr);
			return connection;
		}

		// Needs to be called by endpoints once done with various CRUD actions
		// OR should this be called upon app close?
		public static void SqlServerDisconnect(SqlConnection connection)
		{
			connection.Close();
		}

	}
}

