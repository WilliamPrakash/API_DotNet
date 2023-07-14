//using System;
using MongoDB.Bson;

namespace api.Models
{
	public class Client
	{
		//[BsonId]
		public ObjectId _id { get; set; }
		public string? name { get; set; }
		public string? email { get; set; }
		public Int32? age { get; set; }
		public bool isAdmin { get; set; }
	}
}

