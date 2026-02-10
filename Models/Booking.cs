namespace _2026_peminjaman_ruangan_backend.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public Room? Room { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}