using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mla.ApiMusica.Model { 
    public class Audio
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _ID { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

        [BsonElement("UID")]
        public string? UID { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId AudioFileId { get; set; }
    }
}
