using CineReserveAPI.Data;
using CineReserveAPI.DTOs;
using CineReserveAPI.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CineReserveAPI.Controllers
{
    [ApiController]

    [Route("api/[controller]")]

    public class AuthController
        : ControllerBase
    {
        private readonly AppDbContext
            _context;

        private readonly IConfiguration
            _configuration;

        public AuthController(

            AppDbContext context,

            IConfiguration configuration
        )
        {
            _context = context;

            _configuration =
                configuration;
        }

        // =========================
        // REGISTER
        // =========================

        [HttpPost("register")]

        public async Task<IActionResult>
            Register(RegisterDto dto)
        {
            // CHECK EMAIL

            bool exists =

                await _context.Users

                .AnyAsync(

                    u =>

                    u.Email ==
                    dto.Email
                );

            if (exists)
            {
                return BadRequest(
                    "Email already exists"
                );
            }

            // CREATE USER

            var user = new User
            {
                

                Email =
                    dto.Email,

                PasswordHash =
                    dto.Password,

                Role =
                    "User",

                CreditBalance =
                    5000
            };

            _context.Users
                .Add(user);

            await _context
                .SaveChangesAsync();

            return Ok(new
            {
                message =
                    "Signup Successful"
            });
        }

        // =========================
        // LOGIN
        // =========================

        [HttpPost("login")]

        public async Task<IActionResult>
            Login(LoginDto dto)
        {
            var user =

                await _context.Users

                .FirstOrDefaultAsync(

                    u =>

                    u.Email ==
                    dto.Email &&

                    u.PasswordHash ==
                    dto.Password
                );

            if (user == null)
            {
                return Unauthorized(
                    "Invalid credentials"
                );
            }

            // CLAIMS

            var claims = new[]
            {
                new Claim(
                    ClaimTypes.Name,
                    user.Email
                ),

                new Claim(
                    ClaimTypes.Role,
                    user.Role
                ),

                new Claim(
                    "UserId",
                    user.UserId.ToString()
                )
            };

            // JWT KEY

            var key =

                new SymmetricSecurityKey(

                    Encoding.UTF8.GetBytes(

                        _configuration[
                            "Jwt:Key"
                        ]
                    )
                );

            // CREDENTIALS

            var creds =

                new SigningCredentials(

                    key,

                    SecurityAlgorithms
                        .HmacSha256
                );

            // TOKEN

            var token =
                new JwtSecurityToken(

                    issuer:

                        _configuration[
                            "Jwt:Issuer"
                        ],

                    audience:

                        _configuration[
                            "Jwt:Audience"
                        ],

                    claims:
                        claims,

                    expires:

                        DateTime.Now
                            .AddHours(2),

                    signingCredentials:
                        creds
                );

            return Ok(new
            {
                token =

                    new JwtSecurityTokenHandler()

                    .WriteToken(token),

                role =
                    user.Role,

                email =
                    user.Email
            });
        }
    }
}