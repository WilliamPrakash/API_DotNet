using api.Models;
using MongoDB.Driver;
using System.Data.SqlClient;


namespace api.DAL
{
	public class DatabaseConnect
	{
        // Database Info
        Dictionary<string, string>? localCredentials;
        public static string sqlConnStr = "";
        public IMongoCollection<Client_Mongo>? _clientCollection;

        // Constructor
        public DatabaseConnect()
        {
            string mongoPwd = "";

            GrabLocalDatabaseCredentials creds = new GrabLocalDatabaseCredentials();
            localCredentials = creds.OpenLocalAuthFile();

            // If no DB credentials are found, shut down the application.
            if (localCredentials == null) { System.Environment.Exit(1); }

            if (localCredentials.Count != 0)
            {
                for (int i = 0; i < localCredentials.Keys.Count; i++)
                {
                    if (localCredentials.ElementAt(i).Key == "MongoDB")
                    {
                        mongoPwd = localCredentials.ElementAt(i).Value;
                    }
                    else if (localCredentials.ElementAt(i).Key == "SQLServer")
                    {
                        sqlConnStr = localCredentials.ElementAt(i).Value;
                    }
                }
            }

            // MongoDB
            //if (mongoPwd != "")
            if (false)
            {
                var mongoConnStr = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings:MongoDB").Value;
                mongoConnStr = mongoConnStr.Replace("<password>", mongoPwd);
                // mongodb+srv://willprakash:Jdq_31aFINISH@testcluster.y2gbcvh.mongodb.net/?retryWrites=true&w=majority
                //dbConn.MongoDBConnect(mongoConnStr);
                MongoDBConnect("mongodb+srv://willprakash:Jdq_31aFINISH@testcluster.y2gbcvh.mongodb.net/?authSource=admin");
            }

        }

        public void MongoDBConnect(string mongoConnStr)
		{
			// Create client, connect to DB, connect to collection
            MongoClient client = new MongoClient(mongoConnStr);
			IMongoDatabase db = client.GetDatabase("MainDB");
            _clientCollection = db.GetCollection<Client_Mongo>("TestCluster");

			var test = _clientCollection.Find(x => x.name == "will").ToList();

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

