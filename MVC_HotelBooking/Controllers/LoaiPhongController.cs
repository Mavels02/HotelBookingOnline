using Microsoft.AspNetCore.Mvc;
using MVC_HotelBooking.Models;
using MVC_HotelBooking.ViewModel;
using Newtonsoft.Json;
using System.Text;

namespace MVC_HotelBooking.Controllers
{
    public class LoaiPhongController : Controller
    {
		private readonly HttpClient _httpClient;

		public LoaiPhongController()
		{
			_httpClient = new HttpClient();
			_httpClient.BaseAddress = new Uri("https://localhost:7077/api/");
		}

		public async Task<IActionResult> Index()
		{
			var response = await _httpClient.GetAsync("LoaiPhong");
			var data = await response.Content.ReadAsStringAsync();
			var loaiPhongs = JsonConvert.DeserializeObject<List<LoaiPhongViewModel>>(data);
			return View(loaiPhongs);
		}

		public IActionResult Create() => View();

		[HttpPost]
		public async Task<IActionResult> Create(LoaiPhongViewModel model)
		{
			var json = JsonConvert.SerializeObject(model);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync("LoaiPhong", content);
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Edit(int id)
		{
			var response = await _httpClient.GetAsync($"LoaiPhong/{id}");
			var data = await response.Content.ReadAsStringAsync();
			var model = JsonConvert.DeserializeObject<LoaiPhongViewModel>(data);
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(LoaiPhongViewModel model)
		{
			var json = JsonConvert.SerializeObject(model);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			await _httpClient.PutAsync($"LoaiPhong/{model.MaLP}", content);
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Delete(int id)
		{
			await _httpClient.DeleteAsync($"LoaiPhong/{id}");
			return RedirectToAction("Index");
		}
	}
}
