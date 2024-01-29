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
    public class ExtensionService 
    {
        private readonly DataContext _context;

        public ExtensionService(DataContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Extension>>> GetExtensions()
        {
            return await _context.Extensions.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Extension>?>?> GetExtension(string name)
        {
            var extension = await _context.Extensions.Include(e => e.Songs)
            .Where(e => e.Name == name).ToListAsync();

            if (extension == null) {
                return null;
            }

            return extension;
        }

        public async Task<ActionResult<Extension>?> PostExtension(Extension extension)
        {
            _context.Extensions.Add(extension);
            await _context.SaveChangesAsync();

            return null;
        }

        public async Task<IActionResult?> PutExtension(string name, Extension extension)
        {
            if (name != extension.Name)
            {
                return null;
            }

            _context.Entry(extension).State = EntityState.Modified;

            try 
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return null;
        }

        public async Task<IActionResult?> DeleteExtension(string name)
        {
            var extension = await _context.Extensions.FindAsync(name);
            if (extension == null)
            {
                return null;
            }

            _context.Extensions.Remove(extension);
            await _context.SaveChangesAsync();

            return null;
        }

        public async Task<bool> UpdateExtensionsForSong(Guid songUID, ICollection<Extension> newExtensions)
        {
            try
            {
                var existingExtensions = await GetExistingExtensionsForSong(songUID);
                RemoveExtensions(existingExtensions, newExtensions, songUID);
                AddOrUpdateExtensions(existingExtensions, newExtensions);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                // Handle any error as needed.
                return false;
            }
        }

        private async Task<List<Extension>> GetExistingExtensionsForSong(Guid songUID)
        {
            return await _context.Extensions
                .Include(e => e.Songs) // Include Songs collection for eager loading
                .Where(e => e.Songs.Any(s => s.UID == songUID))
                .ToListAsync();
        }

        private void RemoveExtensions(List<Extension> existingExtensions, ICollection<Extension> newExtensions, Guid songUID)
        {
            var extensionsToRemove = existingExtensions
                .Where(existingExtension => !newExtensions.Any(newExtension => newExtension.Name == existingExtension.Name))
                .ToList();

            foreach (var extension in extensionsToRemove)
            {
                var songToRemove = extension.Songs.FirstOrDefault(s => s.UID == songUID);

                if (songToRemove != null)
                {
                    // Remove the specific song with the given songUID from the Songs collection
                    extension.Songs.Remove(songToRemove);
                }
            }
        }

        private void AddOrUpdateExtensions(List<Extension> existingExtensions, ICollection<Extension> newExtensions)
        {
            foreach (var newExtension in newExtensions)
            {
                var existingExtension = existingExtensions.FirstOrDefault(e => e.Name == newExtension.Name);

                if (existingExtension == null)
                {
                    _context.Extensions.Add(newExtension);
                }
                else
                {
                    existingExtension.Songs.Add(newExtension.Songs.FirstOrDefault());
                    // Update any other properties of the extension if needed
                }
            }
        }
    }
}