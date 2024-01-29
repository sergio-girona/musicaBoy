namespace ApiMongoMusica.Classes
{
    public class MongoMusicInfoSettings
    {
        public string ConnectionString { get; set; } =null!;
        public string DatabaseName { get; set; } =null!;
        public string LletresCollection { get; set; }=null!;
        public string HistorialCollection { get; set; }=null!;
    }
}