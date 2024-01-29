using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiMongoMusica.Classes.Models
{
    public class Historial
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? UidUser { get; set; }
        public string? UidSong { get; set; }
        public string? TitleSong { get; set; }
    }
}