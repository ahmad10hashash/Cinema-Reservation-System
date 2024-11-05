using System.ComponentModel.DataAnnotations.Schema;

namespace CRS.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public string MovieDescription { get; set; }
        public string Genre { get; set; }
        public string Duration { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Language { get; set; }
        public string Subtitle { get; set; }
        public string TrailerPath { get; set; }
        public string? PosterPath { get; set; }
        [NotMapped]
        public IFormFile MovieFile { get; set; }
    }
}
