namespace _2026_peminjaman_ruangan_backend.DTOs
{
    public class CreateBookingDTO
    {
        public int RoomId { get; set; }
        public int CustomerId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}