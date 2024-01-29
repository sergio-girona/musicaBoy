using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiMusicInfo.Data;
using apiMusicInfo.Models;

namespace apiMusicInfo.Controllers.Services
{
    public class SongService
    {
        private readonly DataContext _context;

        public SongService(DataContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            return await _context.Songs.ToListAsync();
        }

        public async Task<IEnumerable<Song>?> GetSong(string uid)
        {
            Guid guidUid = Guid.Parse(uid);
            var song = await _context.Songs
            .Include(s => s.Extensions)
            .Include(s => s.Plays)
            .Include(s => s.Plays)
            .Include(s => s.Albums)
            .Where(s => s.UID == guidUid)
            .ToListAsync();

            if (song == null)
            {
                return null;
            }

            return song;
        }

        public async Task<ActionResult<Song>?> PostSong(Song song)
        {
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            return null;
        }

        public async Task<Song?> UpdateSong(Guid UID, Song updatedSong)
        {
            if (UID != updatedSong.UID)
            {
                return null;
            }

            var existingSong = await _context.Songs.FindAsync(UID);

            if (existingSong == null)
            {
                return null;
            }

            UpdateSongProperties(existingSong, updatedSong);

            try 
            {
                await _context.SaveChangesAsync();
                return existingSong;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ApplicationException("Concurrency conflict occurred during song update.", ex);
            }
        }

        private void UpdateSongProperties(Song existingSong, Song updatedSong)
        {
            existingSong.Title = updatedSong.Title;
            existingSong.Language = updatedSong.Language;
            existingSong.Duration = updatedSong.Duration;
        }
        
        public async Task<ActionResult<Song>?> DeleteSong(Guid UID)
        {
            var song = await _context.Songs.FindAsync(UID);
            if (song == null)
            {
                return null;
            }
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return song;
        }
    }
}