using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FitnessAppAPI.Models
{
    public class CheckIns
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? _id { get; set; }
        public DateTime? checkin_date { get; set; } = DateTime.MinValue;

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? user_membership_id { get; set; }
    }
}
