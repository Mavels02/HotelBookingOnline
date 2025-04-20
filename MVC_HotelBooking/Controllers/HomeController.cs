using Microsoft.AspNetCore.Mvc;
using MVC_HotelBooking.Models;
using MVC_HotelBooking.ViewModel;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq.Dynamic.Core;
using System.Text.Json;

namespace MVC_HotelBooking.Controllers
{
	public class HomeController : Controller
	{

		private readonly HttpClient _httpClient;

		public HomeController(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient();
			_httpClient.BaseAddress = new Uri("http://localhost:40841/");
		}


		public async Task<IActionResult> Index(int page = 1, int? loaiPhong = null, string? status = null, decimal? min = null, decimal? max = null)
		{
			string url = $"api/Phong/page?page={page}&pageSize=6";

			var queryParams = new List<string>();
			if (loaiPhong.HasValue) queryParams.Add($"loaiPhong={loaiPhong}");
			if (!string.IsNullOrEmpty(status)) queryParams.Add($"status={status}");
			if (min.HasValue) queryParams.Add($"min={min}");
			if (max.HasValue) queryParams.Add($"max={max}");
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
				MaxPrice = max
			};

			return View(viewModel);
		}
		public async Task<IActionResult> Details(int id)
		{
			var response = await _httpClient.GetFromJsonAsync<PhongViewModel>($"api/Phong/{id}");
			if (response == null) return NotFound();

			return View(response);
		}
	}
}
