using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MVC_HotelBooking.Models;
using MVC_HotelBooking.ViewModel;
using Newtonsoft.Json;
using MVC_HotelBooking.Session;

namespace MVC_HotelBooking.Controllers
{
	public class DatPhongController : Controller
	{
		private readonly HttpClient _httpClient;

		public DatPhongController(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient();
			_httpClient.BaseAddress = new Uri("http://localhost:40841/");
		}


		public async Task<IActionResult> Index(int page = 1, int? loaiPhong = null, string? status = null, decimal? min = null, decimal? max = null, string? search = null)
		{
			string url = $"api/Phong/page?page={page}&pageSize=6";
			if (!string.IsNullOrEmpty(search))
			{
				url += $"&search={search}";
			}
			var queryParams = new List<string>();
			if (loaiPhong.HasValue) queryParams.Add($"loaiPhong={loaiPhong}");
			if (!string.IsNullOrEmpty(status)) queryParams.Add($"status={status}");
			if (min.HasValue) queryParams.Add($"min={min}");
			if (max.HasValue) queryParams.Add($"max={max}");
			if (!string.IsNullOrEmpty(search)) queryParams.Add($"search={search}");
			if (queryParams.Any()) url += "&" + string.Join("&", queryParams);

			var response = await _httpClient.GetFromJsonAsync<ApiPhongResponse>(url);
			var loaiPhongs = await _httpClient.GetFromJsonAsync<List<LoaiPhong>>("api/LoaiPhong");

			var viewModel = new HomeViewModel
			{
				Rooms = response?.Rooms ?? new(),
				CurrentPage = response?.CurrentPage ?? 1,
				TotalPages = response?.TotalPages ?? 1,
				LoaiPhongs = loaiPhongs ?? new(),
				SelectedLoaiPhong = loaiPhong,
				SelectedStatus = status,
				MinPrice = min,
				MaxPrice = max,
				SearchQuery = search
			};

			return View(viewModel);
		}
		
		public async Task<IActionResult> DatPhongChiTiet(int maPhong)
		{
			var response = await _httpClient.GetAsync($"api/DatPhong/{maPhong}");
			if (!response.IsSuccessStatusCode) return NotFound();

			var json = await response.Content.ReadAsStringAsync();
			var model = JsonConvert.DeserializeObject<DatPhongViewModel>(json);

			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> DatPhong(DatPhongViewModel model)
		{
			var EmailND = HttpContext.Session.GetString("Email");
			var Id = HttpContext.Session.GetInt32("Id");

			if (string.IsNullOrEmpty(EmailND))
			{
				return RedirectToAction("Login", "Account");
			}

		
			  
			var response = await _httpClient.PostAsJsonAsync("api/DatPhong/dat-phong", model);
			if (response.IsSuccessStatusCode)
			{   

				var result = await response.Content.ReadFromJsonAsync<DatPhong>();
				var cart = HttpContext.Session.Get<List<DatPhong>>("Cart") ?? new List<DatPhong>();
				cart.Add(result);
				HttpContext.Session.Set("Cart", cart);
				return RedirectToAction("Index", "ThanhToan", new { id = result.MaDP });
			}

			var error = await response.Content.ReadAsStringAsync();

			// Cố gắng tách riêng thông báo lỗi "message" nếu có
			try
			{
				using var doc = JsonDocument.Parse(error);
				if (doc.RootElement.TryGetProperty("message", out var messageElement))
				{
					var message = messageElement.GetString();
					ModelState.AddModelError("", message);
				}
				else
				{
					ModelState.AddModelError("", "Đã xảy ra lỗi không xác định.");
				}
			}
			catch
			{
				ModelState.AddModelError("", "Đã xảy ra lỗi khi xử lý phản hồi từ máy chủ.");
			} 
			return View("DatPhongChiTiet", model);

		}

		public IActionResult Cart()
		{
			var cart = HttpContext.Session.Get<List<DatPhong>>("Cart") ?? new List<DatPhong>();
			return View(cart);
		}

	}
}
