using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API_HotelBooking.Migrations
{
    /// <inheritdoc />
    public partial class updateloaiphong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DichVus",
                keyColumn: "MaDV",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DichVus",
                keyColumn: "MaDV",
                keyValue: 2);

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

            migrationBuilder.RenameColumn(
                name: "LoaiPhongName",
                table: "LoaiPhongs",
                newName: "TenLoai");

            migrationBuilder.AlterColumn<string>(
                name: "TrangThai",
                table: "Phongs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenLoai",
                table: "LoaiPhongs",
                newName: "LoaiPhongName");

            migrationBuilder.AlterColumn<string>(
                name: "TrangThai",
                table: "Phongs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

         
           
        }
    }
}
