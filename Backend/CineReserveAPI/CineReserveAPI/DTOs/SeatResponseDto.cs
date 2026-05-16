namespace CineReserveAPI.DTOs
{
    public class SeatResponseDto
    {
        public int SeatId { get; set; }

        public string RowNumber { get; set; }

        public int SeatNumber { get; set; }

        public string SeatType { get; set; }

        public bool IsBooked { get; set; }
    }
}