using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FitnessAppAPI.Models
{
    public class UserMemberships
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? _id { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? user_id { get; set; }

        public string? name { get; set; }
        public string? CNP { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? membership_id { get; set; }

        public string? megnevezes { get; set; }
        public DateTime? purchase_date { get; set; } = DateTime.UtcNow;
        public int? checkin_count { get; set; } = 0;
        public bool? active { get; set; } = true;
    }
}
