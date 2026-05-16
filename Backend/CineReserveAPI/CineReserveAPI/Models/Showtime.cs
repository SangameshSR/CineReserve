namespace CineReserveAPI.Models
{
    public class Showtime
    {
        public int ShowtimeId { get; set; }

        public int MovieId { get; set; }

        public DateTime ShowDate { get; set; }

        public TimeSpan ShowTime { get; set; }

        public string HallName { get; set; }
    }
}