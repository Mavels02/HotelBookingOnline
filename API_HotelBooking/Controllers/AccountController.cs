using System.Security.Cryptography;
using System.Text;
using API_HotelBooking.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_HotelBooking.Requests;

namespace API_HotelBooking.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly AppDbContext _context;
		public AccountController(AppDbContext context)
		{
			_context = context;
		}
		[HttpPost("register")]
		
		public IActionResult Register([FromBody] Register userregister)
		{
			// Kiểm tra người dùng đã tồn tại chưa
			var existingUser = _context.NguoiDungs.FirstOrDefault(u => u.Email == userregister.Email);
			if (existingUser != null)
			{
			
				return BadRequest("Email đã tồn tại.");
			}

			// Kiểm tra số điện thoại hợp lệ
			if (!long.TryParse(userregister.SDT, out _) || userregister.SDT.Length != 10)
			{
				return BadRequest("Số điện thoại phải là số và có 10 chữ số.");
			}

			// Kiểm tra mật khẩu
			if (userregister.MatKhau.Length <= 6)
			{
				return BadRequest("Mật khẩu phải dài hơn 6 ký tự.");
			}
			var hashedPassword = HashPassword(userregister.MatKhau);
			var user = new NguoiDung
			{
				Ten = userregister.Ten,
				Email = userregister.Email,
				VaiTro = "Customer",
				SDT = userregister.SDT,
				MatKhau = hashedPassword
			};

			_context.NguoiDungs.Add(user);
			_context.SaveChanges();

			return Ok(new { message = "Đăng ký thành công!" });
		}

		// API đăng nhập người dùng
		[HttpPost("login")]
		public IActionResult Login([FromBody] Login loginRequest)
		{
			var user = _context.NguoiDungs.FirstOrDefault(u => u.Email == loginRequest.Email);
			if (user == null || !VerifyPassword(loginRequest.MatKhau, user.MatKhau))
				return Unauthorized("Invalid credentials");

			return Ok(new { user.MaND, user.Ten, user.Email, user.VaiTro });
		}

		private string HashPassword(string password)
		{
			using (var sha256 = SHA256.Create())
			{
				var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
				return Convert.ToBase64String(hashBytes);
			}
		}

		private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
		{
			var enteredPasswordHash = HashPassword(enteredPassword);
			return enteredPasswordHash == storedPasswordHash;
		}
	}
}

	


