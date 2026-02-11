using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _2026_peminjaman_ruangan_backend.Data;
using _2026_peminjaman_ruangan_backend.Models;
using _2026_peminjaman_ruangan_backend.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace _2026_peminjaman_ruangan_backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingsController(AppDbContext context)
        {
            _context = context;
        }

        // 1. GET: api/bookings (Ambil semua bokingan + Data User)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDTO>>> GetBookings(int? roomId, DateTime? fromDate, DateTime? toDate)
        {
            // panggil Room DAN Customer biar infonya lengkap
            var query = _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.Customer)
                .AsQueryable();

            if (roomId.HasValue) query = query.Where(b => b.RoomId == roomId.Value);
            if (fromDate.HasValue) query = query.Where(b => b.StartTime >= fromDate.Value);
            if (toDate.HasValue) query = query.Where(b => b.EndTime <= toDate.Value);

            return await query.Select(b => new BookingDTO
            {
                Id = b.Id,
                RoomId = b.RoomId,
                RoomName = b.Room != null ? b.Room.Name : null,
                CustomerId = b.CustomerId,
                CustomerName = b.Customer != null ? b.Customer.Username : "Unknown",
                StartTime = b.StartTime,
                EndTime = b.EndTime
            }).ToListAsync();
        }

        // 2. POST: api/bookings (Boking Ruangan Pake Token JWT)
        [HttpPost]
        public async Task<ActionResult<BookingDTO>> PostBooking(CreateBookingDTO dto)
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr))
                return Unauthorized("Token tidak valid!");

            int loggedInUserId = int.Parse(userIdStr);

            var roomExists = await _context.Rooms.AnyAsync(r => r.Id == dto.RoomId);
            if (!roomExists) return BadRequest("Ruangan tidak ditemukan!");

            // Logika Anti-Bentrok
            var isBentrok = await _context.Bookings.AnyAsync(b =>
                b.RoomId == dto.RoomId &&
                dto.StartTime < b.EndTime &&
                dto.EndTime > b.StartTime);

            if (isBentrok)
                return BadRequest("Jadwal bentrok dengan booking yang sudah ada!");

            var booking = new Booking
            {
                RoomId = dto.RoomId,
                CustomerId = loggedInUserId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Booking berhasil dibuat!", id = booking.Id });
        }

        // 3. DELETE: api/bookings/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound("Data booking tidak ditemukan!");

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 1.5 GET: api/bookings/my-bookings (khusus riwayat user yang login)
        [HttpGet("my-bookings")]
        public async Task<ActionResult<IEnumerable<BookingDTO>>> GetMyBookings()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr)) return Unauthorized("Login terlebih dahulu!");

            int userId = int.Parse(userIdStr);

            // cuma ambil data bokingan yang CustomerId nya cocok sama user ini
            var myBookings = await _context.Bookings
                .Include(b => b.Room)
                .Where(b => b.CustomerId == userId)
                .Select(b => new BookingDTO
                {
                    Id = b.Id,
                    RoomId = b.RoomId,
                    RoomName = b.Room != null ? b.Room.Name : null,
                    CustomerId = b.CustomerId,
                    CustomerName = b.Customer != null ? b.Customer.Username : null,
                    StartTime = b.StartTime,
                    EndTime = b.EndTime
                }).ToListAsync();

            return Ok(myBookings);
        }
    }
}