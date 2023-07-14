using System;
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
			var db = client.GetDatabase("MainDB");
			Console.WriteLine(db);
		}
	}
}

