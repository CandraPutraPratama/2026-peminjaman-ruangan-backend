namespace _2026_peminjaman_ruangan_backend.DTOs
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string? RoomName { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; } 
    }
}