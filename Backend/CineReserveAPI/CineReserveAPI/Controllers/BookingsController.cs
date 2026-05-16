using CineReserveAPI.Data;
using CineReserveAPI.DTOs;
using CineReserveAPI.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineReserveAPI.Controllers
{
    [ApiController]

    [Route("api/[controller]")]

    public class BookingsController
        : ControllerBase
    {
        private readonly AppDbContext
            _context;

        public BookingsController(
            AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // CREATE BOOKING
        // =========================

        [HttpPost]

        public async Task<IActionResult>
            CreateBooking(
                CreateBookingDto dto)
        {
            using var transaction =

                await _context.Database
                    .BeginTransactionAsync();

            try
            {
                // FIND USER

                var user =

                    await _context.Users

                    .FirstOrDefaultAsync(

                        u =>

                        u.UserId ==
                        dto.UserId
                    );

                if (user == null)
                {
                    return BadRequest(
                        "User not found"
                    );
                }

                decimal totalAmount = 0;

                // CHECK SEATS

                foreach (
                    var seat in dto.Seats
                )
                {
                    bool alreadyBooked =

                        await _context
                            .TicketDetails

                        .AnyAsync(

                            t =>

                            t.ShowtimeId ==
                            dto.ShowtimeId &&

                            t.RowNumber ==
                            seat.RowNumber &&

                            t.SeatNumber ==
                            seat.SeatNumber
                        );

                    if (alreadyBooked)
                    {
                        return BadRequest(

                            $"Seat " +

                            $"{seat.RowNumber}" +

                            $"{seat.SeatNumber} " +

                            $"already booked"
                        );
                    }

                    // VIP PRICE

                    if (
                        seat.RowNumber == "G" ||

                        seat.RowNumber == "H"
                    )
                    {
                        totalAmount += 350;
                    }
                    else
                    {
                        totalAmount += 200;
                    }
                }

                // BALANCE CHECK

                if (
                    user.CreditBalance <
                    totalAmount
                )
                {
                    return BadRequest(
                        "Insufficient balance"
                    );
                }

                // DEDUCT USER BALANCE

                user.CreditBalance -=
                    totalAmount;

                // CREATE BOOKING

                var booking = new Booking
                {
                    UserId =
                        dto.UserId,

                    ShowtimeId =
                        dto.ShowtimeId,

                    TotalAmount =
                        totalAmount,

                    BookingReference =

                        Guid.NewGuid()
                        .ToString()
                        .Substring(0, 8),

                    BookingDate =
                        DateTime.Now
                };

                _context.Bookings
                    .Add(booking);

                await _context
                    .SaveChangesAsync();

                // CREATE TICKETS

                foreach (
                    var seat in dto.Seats
                )
                {
                    decimal price =

                        (
                            seat.RowNumber == "G" ||

                            seat.RowNumber == "H"
                        )

                        ? 350

                        : 200;

                    var ticket =
                        new TicketDetail
                        {
                            BookingId =
                                booking.BookingId,

                            ShowtimeId =
                                dto.ShowtimeId,

                            RowNumber =
                                seat.RowNumber,

                            SeatNumber =
                                seat.SeatNumber,

                            Price =
                                price
                        };

                    _context.TicketDetails
                        .Add(ticket);
                }

                await _context
                    .SaveChangesAsync();

                await transaction
                    .CommitAsync();

                return Ok(new
                {
                    message =
                        "Booking successful",

                    bookingReference =
                        booking.BookingReference,

                    totalAmount =
                        totalAmount
                });
            }

            catch (Exception ex)
            {
                await transaction
                    .RollbackAsync();

                return StatusCode(
                    500,
                    ex.Message
                );
            }
        }

        // =========================
        // GET USER BOOKINGS
        // =========================

        [HttpGet("user")]

        public async Task<IActionResult>
            GetUserBookings()
        {
            var bookings = await (

                from b in _context.Bookings

                join s in _context.Showtimes
                    on b.ShowtimeId equals s.ShowtimeId

                join m in _context.Movies
                    on s.MovieId equals m.MovieId

                select new
                {
                    bookingId =
                        b.BookingId,

                    bookingReference =
                        b.BookingReference,

                    movieTitle =
                        m.Title,

                    showDate =
                        s.ShowDate,

                    showTime =
                        s.ShowTime,

                    totalAmount =
                        b.TotalAmount
                }

            ).ToListAsync();

            var result = bookings.Select(b => new
            {
                b.bookingId,

                b.bookingReference,

                b.movieTitle,

                b.showDate,

                b.showTime,

                b.totalAmount,

                seats = string.Join(

                    ", ",

                    _context.TicketDetails

                    .Where(t =>
                        t.BookingId ==
                        b.bookingId)

                    .Select(t =>

                        t.RowNumber +
                        t.SeatNumber
                    )
                )
            });

            return Ok(result);
        }
    }
}