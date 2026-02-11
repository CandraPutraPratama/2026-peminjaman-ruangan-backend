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
    public class RoomsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoomsController(AppDbContext context)
        {
            _context = context;
        }

        // 1. GET: api/rooms (untuk ambil semua ruangan)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms(string? search)
        {
            var query = _context.Rooms.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(r => r.Name.Contains(search));
            }

            return await query.Select(r => new RoomDTO { Id = r.Id, Name = r.Name, Capacity = r.Capacity }).ToListAsync();
        }

        // 2. POST: api/rooms (untuk tambah ruangan baru)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<RoomDTO>> PostRoom(CreateRoomDTO createDto)
        {
            var room = new Room { Name = createDto.Name, Capacity = createDto.Capacity };
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRooms), new { id = room.Id },
                new RoomDTO { Id = room.Id, Name = room.Name, Capacity = room.Capacity });
        }

        // 3. DELETE: api/rooms/{id} (untuk hapus ruangan)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return NotFound();

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 4. PUT: api/rooms/{id} (untuk update ruangan)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, CreateRoomDTO updateDto)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return NotFound();

            room.Name = updateDto.Name;
            room.Capacity = updateDto.Capacity;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}