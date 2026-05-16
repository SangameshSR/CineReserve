namespace CineReserveAPI.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int UserId { get; set; }

        public int ShowtimeId { get; set; }

        public decimal TotalAmount { get; set; }

        public string BookingReference { get; set; }

        public DateTime BookingDate { get; set; }
    }
}