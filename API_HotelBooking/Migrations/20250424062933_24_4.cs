using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_HotelBooking.Migrations
{
    /// <inheritdoc />
    public partial class _24_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoLuongNguoiToiDa",
                table: "Phongs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoLuongNguoiToiDa",
                table: "Phongs");
        }
    }
}
