using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _2026_peminjaman_ruangan_backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "EndTime", "RoomId", "StartTime", "UserEmail" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2026, 2, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), "candra@pdbl.com" },
                    { 2, new DateTime(2026, 2, 11, 16, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2026, 2, 11, 13, 0, 0, 0, DateTimeKind.Unspecified), "putra@pdbl.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
