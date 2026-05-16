namespace CineReserveAPI.Models
{
    public class Movie
    {
        public int MovieId { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public int DurationMinutes { get; set; }

        public string Language { get; set; }

        public string PosterUrl { get; set; }
    }
}