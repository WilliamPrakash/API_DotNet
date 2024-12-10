using api.Models;
using MongoDB.Driver;
using System.Data.SqlClient;


namespace api.DAL
{
	public class DatabaseConnect
	{
        // Initialize collection (Mongo)
        public IMongoCollection<Client_Mongo>? _clientCollection;

        public void MongoDBConnect(string mongoConnStr)
		{
			// Create client, connect to DB, connect to collection
            MongoClient client = new MongoClient(mongoConnStr);
			IMongoDatabase db = client.GetDatabase("MainDB");
            _clientCollection = db.GetCollection<Client_Mongo>("TestCluster");

			var test = _clientCollection.Find(x => x.name == "will").ToList();

            Console.WriteLine(test);
		}

		public void SQLServerConnect(string sqlConnStr)
		{
			using (SqlConnection connection = new SqlConnection(sqlConnStr))
			{
				// Create a list of Client_SQLs
				List<Client_SQL> clients = new List<Client_SQL>();

                // Source: https://stackoverflow.com/questions/21709305/how-to-directly-execute-sql-query-in-c
                SqlCommand command = new SqlCommand("select * from Master.dbo.Contacts", connection);
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();
				try
				{
					while (reader.Read())
					{
						Client_SQL client = new Client_SQL();
						client.Id = reader.GetInt32(0);
                        client.Name = reader.GetString(1);
                        client.Email = reader.GetString(2);
						client.Occupation = reader.GetString(3);
						clients.Add(client);
                    }
				}
				finally
				{
					reader.Close();
				}
				connection.Close(); // we should probably keep this open while the app is running
			}

		}

	}
}

