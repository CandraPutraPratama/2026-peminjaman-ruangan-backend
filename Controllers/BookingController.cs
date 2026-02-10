using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _2026_peminjaman_ruangan_backend.Data;
using _2026_peminjaman_ruangan_backend.Models;
using _2026_peminjaman_ruangan_backend.DTOs;
using Microsoft.AspNetCore.Authorization;

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

        // 1. GET: api/bookings (untuk mengambil semua jadwal booking)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDTO>>> GetBookings()
        {
            return await _context.Bookings
                .Include(b => b.Room) // untuk mengambil data ruangan
                .Include(b => b.Customer) // untuk mengambil data customer
                .Select(b => new BookingDTO
                {
                    Id = b.Id,
                    RoomId = b.RoomId,
                    RoomName = b.Room != null ? b.Room.Name : null,
                    CustomerId = b.CustomerId,
                    CustomerName = b.Customer != null ? b.Customer.Name : null,
                    StartTime = b.StartTime,
                    EndTime = b.EndTime
                }).ToListAsync();
        }

        // 2. POST: api/bookings (untuk menambah booking baru)
        [HttpPost]
        public async Task<ActionResult<BookingDTO>> PostBooking(CreateBookingDTO dto)
        {
            // untuk memastikan ruangan dan Customernya memang ada
            var roomExists = await _context.Rooms.AnyAsync(r => r.Id == dto.RoomId);
            var customerExists = await _context.Customers.AnyAsync(c => c.Id == dto.CustomerId);
            if (!roomExists) return BadRequest("Ruangan tidak ditemukan, cek lagi ID-nya!");
            if (!customerExists) return BadRequest("Customer tidak ditemukan, cek lagi ID-nya!");

            var isBentrok = await _context.Bookings.AnyAsync(b =>
                b.RoomId == dto.RoomId &&
                dto.StartTime < b.EndTime &&
                dto.EndTime > b.StartTime);

            if (isBentrok)
                return BadRequest("Jadwal bentrok dengan booking yang sudah ada!");

            var booking = new Booking
            {
                RoomId = dto.RoomId,
                CustomerId = dto.CustomerId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookings), new { id = booking.Id }, dto);
        }

        // 3. DELETE: api/bookings/{id} (untuk membatalkan Booking)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound("Data booking tidak ditemukan!");

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}