using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2026_peminjaman_ruangan_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookingToCustomerRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CustomerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "CustomerId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Customers_CustomerId",
                table: "Bookings",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Customers_CustomerId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Bookings");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserEmail",
                value: "candra@pdbl.com");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserEmail",
                value: "putra@pdbl.com");
        }
    }
}
