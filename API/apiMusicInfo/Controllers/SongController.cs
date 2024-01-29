using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiMusicInfo.Data;
using apiMusicInfo.Models;
using apiMusicInfo.Controllers.Services;

namespace apiMusicInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly SongService _SongService;
        private readonly ExtensionService _ExtensionService;


        public SongController(DataContext context)
        {
            _context = context;
            _SongService = new SongService(context);
            _ExtensionService = new ExtensionService(context);
        }
        // GET: api/Song
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            return await _SongService.GetSongs();
        }

        // GET: api/Song/5
        [HttpGet("{UID}")]
        public async Task<IEnumerable<Song>?> GetSong(Guid UID)
        {
            var song = await _SongService.GetSong(UID.ToString());

            if (song == null)
            {
                return null;
            }

            return song;
        }
        // POST: api/Song
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(Song song)
        {
            await _SongService.PostSong(song);

            string stringUID = song.UID.ToString();

            return CreatedAtAction("GetSong", new { UID = stringUID }, song);
        }


        // DELETE: api/Song/5
        [HttpDelete("{UID}")]
        public async Task<IActionResult> DeleteSong(Guid UID)
        {
            var deletedSong = await _SongService.DeleteSong(UID);

            if (deletedSong == null)
            {
                return NotFound();
            }


            return Ok(deletedSong);
        }

        // PUT: api/Song/5
        [HttpPut("{UID}")]
        public async Task<IActionResult> PutSong(Guid UID, Song song)
        {
            if (UID != song.UID)
            {
                return BadRequest();
            }

            var updatedSong = await _SongService.UpdateSong(UID, song);
            var extensionsRemoved = await _ExtensionService.UpdateExtensionsForSong(UID, song.Extensions);
            if(extensionsRemoved){
                return Ok(updatedSong);
            }
            if (updatedSong == null)
            {
                return NotFound();
            }

            return Ok(updatedSong);
        }
    }
}
