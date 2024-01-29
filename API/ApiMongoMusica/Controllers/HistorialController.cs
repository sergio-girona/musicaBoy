using ApiMongoMusica.Classes.Models;
using ApiMongoMusica.Classes.Services;
using ApiMongoMusica.Classes;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace ApiMongoMusica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistorialController : ControllerBase
    {
        private readonly HistorialService _historialService;

        public HistorialController(HistorialService historialService)
        {
            _historialService = historialService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Historial>>> GetAsync() =>
            await _historialService.GetAsync();

        [HttpGet("{id:length(24)}", Name = "Gethistorial")]
        public async Task<ActionResult<Historial>> GetAsync(string id)
        {
            var historial = await _historialService.GetAsync(id);

            if (historial == null)
            {
                return NotFound();
            }

            return historial;
        }

        [HttpPost]
        public async Task<ActionResult<Historial>> CreateAsync([FromBody] Historial historial)
        {
            try
            {
                // Genera un nuevo Id antes de guardar el historial
                historial.Id = ObjectId.GenerateNewId().ToString();

                await _historialService.CreateAsync(historial);
                return CreatedAtRoute("Gethistorial", new { id = historial.Id }, historial);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el historial: {ex.Message}");
            }
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateAsync(string id, Historial historialIn)
        {
            try
            {
                var historial = await _historialService.GetAsync(id);

                if (historial == null)
                {
                    return NotFound();
                }

                await _historialService.UpdateAsync(id, historialIn);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el historial: {ex.Message}");
            }
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                var historial = await _historialService.GetAsync(id);

                if (historial == null)
                {
                    return NotFound();
                }

                await _historialService.RemoveAsync(historial);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar el historial: {ex.Message}");
            }
        }
    }
}