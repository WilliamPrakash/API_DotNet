using api.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace api.DAL
{
	public class DBConnect
	{
		public void GetMongoDBInstance(string mongoConnStr)
		{
			//TestCluster/MainDB.Clients
            MongoClient client = new MongoClient(mongoConnStr);
			IMongoDatabase db = client.GetDatabase("MainDB");
            var clientCollection = db.GetCollection<Client>("Clients");
			//method 1
			var filter1 = Builders<Client>.Filter.Where(x => x.name == "Will");
            var results1 = clientCollection.Find(filter1);

			//method 2
            var filter2 = Builders<Client>.Filter.Eq(y => y.name, "Will");
            // Error: "Unable to authenticate using sasl protocol mechanism..."
            //https://stackoverflow.com/questions/44513786/error-on-mongodb-authentication
            var firstDocument = clientCollection.Find(new BsonDocument()).FirstOrDefault();
			Console.WriteLine(firstDocument.ToString());
			//var results = clientCollection.Find(x => x.name == "Will").ToList<Client>;


            Console.WriteLine("x");
		}
	}
}

