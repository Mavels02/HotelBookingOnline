using Microsoft.AspNetCore.Identity.Data;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MVC_HotelBooking.ViewModel;
using MVC_HotelBooking.Models;
using System.Net.Http;

using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Reflection;

namespace MVC_HotelBooking.Controllers
{
	public class AccountController : Controller
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiBaseUrl = "http://localhost:40841/api";
		private readonly IHttpContextAccessor _contextAccessor;
		public AccountController(HttpClient httpClient, IHttpContextAccessor contextAccessor)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("http://localhost:40841/");
			_contextAccessor = contextAccessor;
		}
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (model.MatKhau != model.ConfirmPassword)
			{
				ModelState.AddModelError(string.Empty, "Mật khẩu và xác nhận mật khẩu không khớp.");
				return View(model);
			}
			if (ModelState.IsValid)
			{
				var json = JsonSerializer.Serialize(model);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				var response = await _httpClient.PostAsync($"{_apiBaseUrl}/Account/Register", content);
				if (response.IsSuccessStatusCode)
				{
					ViewBag.Message = "Đăng ký thành công!";
					return RedirectToAction("Login");
				}
				else
				{
					var error = await response.Content.ReadAsStringAsync();
					ModelState.AddModelError(string.Empty, $"Lỗi: {error}");
					return View(model);
				} // Trả lại view kèm lỗi nếu model không hợp lệ
			}
			return View(model);
		
		}

		// Đăng nhập người dùng
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel login)
		{

			var json = JsonSerializer.Serialize(login);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync($"{_apiBaseUrl}/account/login", content);

			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadAsStringAsync();
				var user = JsonSerializer.Deserialize<NguoiDung>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

				// Lưu session (hoặc cookie nếu muốn)
				HttpContext.Session.SetString("Email", user.Email);
				HttpContext.Session.SetString("UserName", user.Ten);
				HttpContext.Session.SetString("VaiTro", user.VaiTro);

				
				if (user.VaiTro == "Admin")
				{
					return RedirectToAction("Privacy", "Home");
				}
				else if (user.VaiTro == "Receptionist")
				{
					return RedirectToAction("Index", "Receptionist");
				}
				else
				{
					return RedirectToAction("Index", "Home");
				}
			}

			ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không đúng.");
			return View(login);
		}

		// Đăng xuất người dùng
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();  // Xóa session
			return RedirectToAction("Index", "Home");  // Quay lại trang chủ
		}
	}
}
