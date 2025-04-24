using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API_HotelBooking.Migrations
{
    /// <inheritdoc />
    public partial class dataloaiphong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LoaiPhongs",
                keyColumn: "MaLP",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LoaiPhongs",
                keyColumn: "MaLP",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LoaiPhongs",
                keyColumn: "MaLP",
                keyValue: 3);
        }
    }
}
