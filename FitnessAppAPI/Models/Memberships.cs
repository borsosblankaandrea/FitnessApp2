using MongoDB.Bson.Serialization.Attributes;

namespace FitnessAppAPI.Models
{
    public class Memberships
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? _id { get; set; }
        public string? megnevezes { get; set; }
        public int? ar { get; set; }
        public int? hanynapigervenyes { get; set; }
        public int hanybelepesreervenyes { get; set; }
    }
}
