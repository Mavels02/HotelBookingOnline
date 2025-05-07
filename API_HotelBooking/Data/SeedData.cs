using API_HotelBooking.Models;
using System.Security.Cryptography;
using System.Text;

namespace API_HotelBooking.Data
{
	public static class SeedData
	{
		public static async Task Initialize(IServiceProvider serviceProvider, AppDbContext context)
		{
			// Kiểm tra nếu tài khoản Admin đã tồn tại
				if (!context.NguoiDungs.Any(u => u.Email == "admin@hotel.com"))
			{
				// Tạo tài khoản Admin nếu chưa có
				var adminUser = new NguoiDung
				{
					Ten = "Admin",
					Email = "admin@hotel.com", // Email Admin mặc định
					MatKhau = HashPassword("Admin@1234"), // Mật khẩu mặc định
					VaiTro = "Admin" // Vai trò Admin
					,
					SDT = "0375293317"
				};

				// Thêm tài khoản Admin vào cơ sở dữ liệu
				context.NguoiDungs.Add(adminUser);
				await context.SaveChangesAsync();
			}
		}

		// Mã hóa mật khẩu trước khi lưu vào DB
		private static string HashPassword(string password)
		{
			using (var sha256 = SHA256.Create())
			{
				var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
				return Convert.ToBase64String(hashBytes);
			}
		}
	}
}
