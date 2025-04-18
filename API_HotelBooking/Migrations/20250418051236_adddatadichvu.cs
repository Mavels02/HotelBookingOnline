using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API_HotelBooking.Migrations
{
    /// <inheritdoc />
    public partial class adddatadichvu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DichVus",
                columns: new[] { "MaDV", "Gia", "KieuDichVu" },
                values: new object[,]
                {
                    { 1, 200000m, "Dịch vụ phòng VIP" },
                    { 2, 50000m, "Dịch vụ dọn dẹp" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DichVus",
                keyColumn: "MaDV",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DichVus",
                keyColumn: "MaDV",
                keyValue: 2);
        }
    }
}
