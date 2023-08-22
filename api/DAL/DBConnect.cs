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
			Client test = new Client();
            test = clientCollection.Find(x => x.name == "will");

			//I think I need to shove the document into a Client model

            //var filter = Builders<BsonDocument>.Filter.Eq(x => x.age, 32);
            //var documents = clientCollection.Find(filter);

            Console.WriteLine(document);
		}
	}
}

