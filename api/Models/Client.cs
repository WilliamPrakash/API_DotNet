using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

//what is bson? blob i think? modified json?

namespace api.Models
{
	public class Client
	{
		[BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
		//[BsonElement("NAME")] //what does this do... does it allow for a different name than the one in the db?
		public string? name { get; set; }
		public string? email { get; set; }
		public Int32? age { get; set; }
		public bool isAdmin { get; set; }
	}
}

