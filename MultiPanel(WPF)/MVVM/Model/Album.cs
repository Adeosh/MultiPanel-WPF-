using System.ComponentModel.DataAnnotations;

namespace MultiPanel_WPF_.MVVM.Model
{
    public class Album
    {
        [Key]
        public int Id { get; set; }
        public string AlbumTitle { get; set; }
        public string Artist { get; set; }
        public string Year { get; set; }
        public string ImageLink { get; set; }
    }
}
