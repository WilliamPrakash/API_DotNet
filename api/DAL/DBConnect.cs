using System;
using api.Models;
using MongoDB.Driver;

namespace api.DAL
{
	public class DBConnect
	{
		/*private readonly IConfiguration _configuration;
		public DBConnect(IConfiguration config)
		{
			_configuration = config;
		}*/

		public void GetMongoDBInstance(string mongoConnStr)
		{
            var client = new MongoClient(mongoConnStr);
			// Database
			var db = client.GetDatabase("MainDB");
            // Collection
            MongoClientSettings settings = new MongoClientSettings();
			var collection = db.GetCluster("Clients");
            var filter = Builders<Client>.Filter.Empty;
			var clients = db.Find(filter).FirstOrDefault();

            Console.WriteLine(db);
		}
	}
}

