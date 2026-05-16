using CineReserveAPI.Data;
using CineReserveAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineReserveAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeatsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SeatsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("showtime/{showtimeId}")]
        public async Task<IActionResult> GetSeatsByShowtime(int showtimeId)
        {
            // Get already booked seats
            var bookedSeats = await _context.TicketDetails
                .Where(t => t.ShowtimeId == showtimeId)
                .Select(t => new
                {
                    t.RowNumber,
                    t.SeatNumber
                })
                .ToListAsync();

            // Get all seats
            var allSeats = await _context.Seats.ToListAsync();

            // Prepare response
            var seats = allSeats.Select(s => new SeatResponseDto
            {
                SeatId = s.SeatId,
                RowNumber = s.RowNumber,
                SeatNumber = s.SeatNumber,
                SeatType = s.SeatType,

                IsBooked = bookedSeats.Any(b =>
                    b.RowNumber == s.RowNumber &&
                    b.SeatNumber == s.SeatNumber)
            })
            .ToList();

            return Ok(seats);
        }
    }
}