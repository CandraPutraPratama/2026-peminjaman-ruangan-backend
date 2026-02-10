# 2026-peminjaman-ruangan-backend

## Deskription
Sistem backend profesional yang dirancang khusus untuk mengelola peminjaman ruangan di lingkungan kampus secara terpusat. Proyek ini berfokus pada efisiensi pemantauan status dan akurasi data peminjaman agar tidak terjadi miss atau double-booking.

## 🚀 Tech Stack
- **Framework**: .NET 10.0 Web API
- **Database**: SQL Server (Docker Container)
- **ORM**: Entity Framework Core
- **Security**: JWT Authentication & BCrypt Password Hashing
- **Testing**: REST Client (.http files)

## ✨ Fitur Utama
1. **Security**: Sistem login dengan token JWT. Endpoint sensitif terkunci rapat!
2. **Anti-Collision Logic**: Validasi boking ruangan cerdas. Tidak ada jadwal bentrok!
3. **Advanced Filtering**: Pencarian ruangan, customer, dan filter bokingan berdasarkan tanggal.
4. **Data Integrity**: Relasi database yang solid antara Room, Customer, dan Booking.

## 🛠️ Cara Setup (3 Langkah Cepat)

### 1. Jalankan Database (Docker)
Pastikan Docker Desktop sudah jalan, lalu eksekusi:
```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourPassword123!" -p 1433:1433 -d [mshub.microsoft.com/mssql/server:2022-latest](https://mshub.microsoft.com/mssql/server:2022-latest)
