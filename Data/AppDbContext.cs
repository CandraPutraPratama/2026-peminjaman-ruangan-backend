using Microsoft.EntityFrameworkCore;
using _2026_peminjaman_ruangan_backend.Models;

namespace _2026_peminjaman_ruangan_backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Name = "Ruang Rapat Utama", Capacity = 20 },
                new Room { Id = 2, Name = "Lab Komputer 1", Capacity = 40 },
                new Room { Id = 3, Name = "Auditorium Mini", Capacity = 100 }
            );
        }
    }
}