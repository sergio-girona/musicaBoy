using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mla.ApiMusica.Model;
using mla.ApiMusica.Services;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using ProjecteV2.ApiMongoDB;

namespace mla.ApiMusica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudioController : ControllerBase
    {
        private readonly AudioService _audioService;
        private readonly ILogger<AudioController> _logger; 

        // Inyectamos ILogger en el constructor
        public AudioController(AudioService audioService, ILogger<AudioController> logger)
        {
            _audioService = audioService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> GuardarAudio([FromForm] AudioUploadModel audioModel)
        {
            try 
            {
                var audio = new Audio 
                {
                    UID = audioModel.Uid.Replace("\"", "")
                };

                // Penjar l'arxiu d'audio a GridFS
                var uploadOptions = new GridFSUploadOptions
                {
                    Metadata = new BsonDocument("contentType", audioModel.Audio.ContentType)
                };

                using (var stream = audioModel.Audio.OpenReadStream())
                {
                    var fileId = await _audioService.UploadAudioAsync(audio.UID, stream, uploadOptions); //audioModel.Audio.FileName
                    audio.AudioFileId = fileId;
                }

                await _audioService.CreateAsync(audio);

                return Ok(new { Message = "Canción y archivo de audio subidos con éxito." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error internt del servidor", Error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodosLosAudios()
        {
            try
            {
                var nombres = await _audioService.ObtenerTodosLosNombresDeAudiosAsync();

                // Aseguramos que 'nombres' no sea nulo antes de retornar
                if (nombres != null)
                {
                    return Ok(nombres);
                }

                // Manejo si 'nombres' es nulo
                return NotFound();
            }
            catch (Exception ex)
            {
                // Logeamos la excepción
                _logger.LogError(ex, "Error al obtener todos los audios");
                return BadRequest($"Error al obtener todos los audios: {ex.Message}");
            }
        }

        [HttpGet("{nombre}")]
        public async Task<IActionResult> ObtenerAudio(string nombre)
        {
            try
            {
                var contenido = await _audioService.ObtenerAudioAsync(nombre);

                // Aseguramos que 'contenido' no sea nulo antes de retornar
                if (contenido != null)
                {
                    return File(contenido, "application/octet-stream");
                }

                // Manejo si 'contenido' es nulo
                return NotFound();
            }
            catch (Exception ex)
            {
                // Logeamos la excepción
                _logger.LogError(ex, "Error al obtener el audio");
                return BadRequest($"Error al obtener el audio: {ex.Message}");
            }
        }
    }
}
