namespace CineReserveAPI.DTOs
{
    public class CreateBookingDto
    {
        public int UserId { get; set; }

        public int ShowtimeId { get; set; }

        public List<SeatSelectionDto> Seats { get; set; }
    }

    public class SeatSelectionDto
    {
        public string RowNumber { get; set; }

        public int SeatNumber { get; set; }
    }
}