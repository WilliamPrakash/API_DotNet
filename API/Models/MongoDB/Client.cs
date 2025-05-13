using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// BSON: binary JSON

namespace API.Models.MongoDB;

	public class Client
	{
		[BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId _id { get; set; }
		public string? name { get; set; }
		public string? email { get; set; }
		public int? age { get; set; }
		public bool isAdmin { get; set; }
		/*[BsonElement("_id")]
    public ObjectId _id { get; set; }
    [BsonElement("name")]
    public string? name { get; set; }
    [BsonElement("email")]
    public string? email { get; set; }
    [BsonElement("age")]
    public Int32? age { get; set; }
    [BsonElement("isAdmin")]
    public bool isAdmin { get; set; }*/
}

