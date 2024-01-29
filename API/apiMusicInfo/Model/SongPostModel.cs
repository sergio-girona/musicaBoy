
using System.ComponentModel.DataAnnotations;

namespace apiMusicInfo.Models
{
    public class SongPostModel
    {
        [Key]
        public string UID { get; set; }
        public string Title { get; set; }
    }
}
