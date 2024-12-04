using api.Models;
using MongoDB.Driver;
using System.Data.SqlClient;


namespace api.DAL
{
	public class DatabaseConnect
	{
        // Initialize collection (Mongo)
        public IMongoCollection<Client>? _clientCollection;

        public void MongoDBConnect(string mongoConnStr)
		{
			// Create client, connect to DB, connect to collection
            MongoClient client = new MongoClient(mongoConnStr);
			IMongoDatabase db = client.GetDatabase("MainDB");
            _clientCollection = db.GetCollection<Client>("TestCluster");

			var test = _clientCollection.Find(x => x.name == "will").ToList();

            Console.WriteLine(test);
		}

		public void SQLServerConnect(string sqlConnStr)
		{
			using (SqlConnection connection = new SqlConnection(sqlConnStr))
			{
                // Source: https://stackoverflow.com/questions/21709305/how-to-directly-execute-sql-query-in-c
                SqlCommand command = new SqlCommand("select * from Master.dbo.Contacts", connection);
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();
				try
				{
					while (reader.Read())
					{
						Console.WriteLine(reader.GetInt32(0));
						Console.WriteLine(reader.GetString(1));
						Console.WriteLine(reader.GetString(2));
                        Console.WriteLine(reader.GetString(3));
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

