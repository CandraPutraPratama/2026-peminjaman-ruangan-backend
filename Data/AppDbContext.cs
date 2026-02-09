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

            // 1. Data Seeding buat Rooms
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Name = "Ruang Rapat Utama", Capacity = 20 },
                new Room { Id = 2, Name = "Lab Komputer 1", Capacity = 40 },
                new Room { Id = 3, Name = "Auditorium Mini", Capacity = 100 }
            );

            // 2. Data Seeding buat Bookings
            modelBuilder.Entity<Booking>().HasData(
                new Booking
                {
                    Id = 1,
                    RoomId = 1, // Ruang Rapat Utama
                    UserEmail = "candra@pdbl.com",
                    StartTime = new DateTime(2026, 2, 10, 9, 0, 0),
                    EndTime = new DateTime(2026, 2, 10, 11, 0, 0)
                },
                new Booking
                {
                    Id = 2,
                    RoomId = 2, // Lab Komputer 1
                    UserEmail = "putra@pdbl.com",
                    StartTime = new DateTime(2026, 2, 11, 13, 0, 0),
                    EndTime = new DateTime(2026, 2, 11, 16, 0, 0)
                }
            );
        }
    }
}