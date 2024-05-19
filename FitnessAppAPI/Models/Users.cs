using MongoDB.Bson.Serialization.Attributes;

namespace FitnessAppAPI.Models
{
    public class Users
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? _id { get; set; }
        public string? name { get; set; }
        public string? CNP { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? address { get; set; }
        public string? password { get; set; }
        public bool? admin { get; set; } = false;
    }
}
