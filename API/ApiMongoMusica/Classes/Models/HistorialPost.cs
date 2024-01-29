using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiMongoMusica.Classes.Models
{
    public class HistorialPost
    {
        public string UidSong { get; set; } = null!;
        public string TitleSong { get; set; } = null!;
    }
}