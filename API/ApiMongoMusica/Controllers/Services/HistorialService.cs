using ApiMongoMusica.Classes.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiMongoMusica.Classes.Services
{
    public class HistorialService
    {
        private readonly IMongoCollection<Historial> _historial;

        public HistorialService(IOptions<MongoMusicInfoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            _historial = database.GetCollection<Historial>("historial");
        }

        public async Task<List<Historial>> GetAsync() =>
            await _historial.Find(_ => true).ToListAsync();

        public async Task<Historial> GetAsync(string id) =>
            await _historial.Find(h => h.Id == id).FirstOrDefaultAsync();

        public async Task<Historial> CreateAsync(Historial historial)
        {
            await _historial.InsertOneAsync(historial);
            return historial;
        }

        public async Task UpdateAsync(string id, Historial historialIn) =>
            await _historial.ReplaceOneAsync(h => h.Id == id, historialIn);

        public async Task RemoveAsync(Historial historialIn) =>
            await _historial.DeleteOneAsync(h => h.Id == historialIn.Id);
    }
}
