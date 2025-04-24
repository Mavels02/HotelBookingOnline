using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_HotelBooking.Migrations
{
    /// <inheritdoc />
    public partial class adddatphong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoNguoi",
                table: "DatPhongs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoNguoi",
                table: "DatPhongs");
        }
    }
}
