using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_HotelBooking.Migrations
{
    /// <inheritdoc />
    public partial class themmotachophong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MoTa",
                table: "Phongs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoTa",
                table: "Phongs");
        }
    }
}
