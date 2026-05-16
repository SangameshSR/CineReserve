using CineReserveAPI.Data;
using CineReserveAPI.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineReserveAPI.Controllers
{
    [ApiController]

    [Route("api/[controller]")]

    public class ShowtimesController
        : ControllerBase
    {
        private readonly AppDbContext
            _context;

        public ShowtimesController(
            AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET ALL SHOWTIMES
        // =========================

        [HttpGet]

        public async Task<IActionResult>
            GetShowtimes()
        {
            var showtimes =

                await _context.Showtimes
                    .ToListAsync();

            return Ok(showtimes);
        }

        // =========================
        // GET SHOWTIMES BY MOVIE
        // =========================

        [HttpGet("movie/{movieId}")]

        public async Task<IActionResult>
            GetShowtimesByMovie(
                int movieId)
        {
            var showtimes =

                await _context.Showtimes

                    .Where(s =>

                        s.MovieId ==
                        movieId
                    )

                    .ToListAsync();

            return Ok(showtimes);
        }

        // =========================
        // ADD SHOWTIME
        // =========================

        [HttpPost]

        public async Task<IActionResult>
            AddShowtime(
                Showtime showtime)
        {
            _context.Showtimes
                .Add(showtime);

            await _context
                .SaveChangesAsync();

            return Ok(new
            {
                message =
                    "Showtime Added"
            });
        }
    }
}