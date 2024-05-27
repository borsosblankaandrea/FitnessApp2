using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FitnessAppAPI.Models
{
    public class CheckIns
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? _id { get; set; }
        public DateTime checkin_date { get; set; } = DateTime.Now;

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? user_membership_id { get; set; }


        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? user_id { get; set; }
        public string? name { get; set; }
        public string? CNP { get; set; }


        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? membership_id { get; set; }

        public string? megnevezes { get; set; }
    }
}
