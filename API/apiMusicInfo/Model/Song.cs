using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace apiMusicInfo.Models
{
    public class Song
    {
        [Key]
        public Guid UID { get; set; } = Guid.NewGuid();
        
        public string Title { get; set; } = null!;
        
        public string? Language { get; set; }
        public int? Duration { get; set; }
        public ICollection<Song> DerivedVersions { get; set; } = new List<Song>();
        public Song? OriginalSong { get; set; }
        public ICollection<Play> Plays { get; set; } = new List<Play>();
        public ICollection<Extension>? Extensions { get; set; } = new List<Extension>();
        public ICollection<Playlist>? Playlists { get; set; } = new List<Playlist>();
        public ICollection<Album> Albums { get; set; } = new List<Album>();
    }
}
