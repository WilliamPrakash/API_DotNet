using System;
using api.Models;
using MongoDB.Driver;

namespace api.DAL
{
	public class DBConnect
	{
		public void GetMongoDBInstance(string mongoConnStr)
		{
            var client = new MongoClient(mongoConnStr);
			// Database
			var db = client.GetDatabase("MainDB");
            // Collection
            var clientCollection = db.GetCollection<Client>("TestCluster");


            Console.WriteLine(clientCollection);
		}
	}
}

