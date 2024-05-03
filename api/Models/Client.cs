using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// BSON: binary JSON

namespace api.Models
{
	public class Client
	{
		[BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
		public string? name { get; set; }
		public string? email { get; set; }
		public Int32? age { get; set; }
		public bool isAdmin { get; set; }
	}
}

