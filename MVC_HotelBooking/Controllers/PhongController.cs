using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_HotelBooking.Models;
using MVC_HotelBooking.ViewModel;
using Newtonsoft.Json;
using Refit;
using System.Text;
using System.Text.Json;

namespace MVC_HotelBooking.Controllers
{
	public class PhongController : Controller
	{
		private readonly HttpClient _httpClient;


		public PhongController(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient();
			_httpClient.BaseAddress = new Uri("http://localhost:40841/");

		}



		public async Task<IActionResult> Index()
		{
			try
			{
				var rooms = await _httpClient.GetFromJsonAsync<List<PhongViewModel>>("api/Phong");
				return View(rooms);
			}
			catch (Exception ex)
			{
				// log lỗi (hoặc dùng TempData để báo lỗi ra View)
				TempData["Error"] = "Lỗi khi tải danh sách phòng.";
				return View(new List<PhongViewModel>());
			}
		}

		public async Task<IActionResult> Create()
		{
			var model = new PhongViewModel
			{
				LoaiPhongs = await GetLoaiPhongDropdown()
			};
			return View(model);
		}

		// POST: Tạo phòng
		[HttpPost]
		public async Task<IActionResult> Create(PhongViewModel model)
		{
			if (!ModelState.IsValid)
			{
				model.LoaiPhongs = await GetLoaiPhongDropdown();
				return View(model);
			}

			if (model.ImageFile != null)
			{
				var imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image");
				if (!Directory.Exists(imageFolderPath))
				{
					Directory.CreateDirectory(imageFolderPath);
				}

				var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
				var filePath = Path.Combine(imageFolderPath, fileName);

				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await model.ImageFile.CopyToAsync(stream);
				}

				model.ImageUrl = fileName;
			}

			var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
			var res = await _httpClient.PostAsync($"api/Phong", content);

			if (res.IsSuccessStatusCode) return RedirectToAction("Index");

			ModelState.AddModelError("", "Thêm phòng thất bại.");
			model.LoaiPhongs = await GetLoaiPhongDropdown();
			return View(model);
		}

		// GET: Form chỉnh sửa
		public async Task<IActionResult> Edit(int id)
		{
			var res = await _httpClient.GetAsync($"api/Phong/{id}");
			if (!res.IsSuccessStatusCode) return NotFound();

			var content = await res.Content.ReadAsStringAsync();
			var model = JsonConvert.DeserializeObject<PhongViewModel>(content);
			model.LoaiPhongs = await GetLoaiPhongDropdown();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, PhongViewModel model)
		{
			if (!ModelState.IsValid)
			{
				model.LoaiPhongs = await GetLoaiPhongDropdown();
				return View(model);
			}

			if (model.ImageFile != null)
			{
				var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
				var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image");
				if (!Directory.Exists(uploadPath))
				{
					Directory.CreateDirectory(uploadPath);
				}
				var path = Path.Combine(uploadPath, fileName);

				using (var stream = new FileStream(path, FileMode.Create))
				{
					await model.ImageFile.CopyToAsync(stream);
				}

				model.ImageUrl = fileName;
			}
			else
			{
				// Không upload file mới, giữ ảnh cũ
				var existingPhong = await _httpClient.GetAsync($"api/Phong/{id}");
				if (existingPhong.IsSuccessStatusCode)
				{
					var existingContent = await existingPhong.Content.ReadAsStringAsync();
					var existingModel = JsonConvert.DeserializeObject<PhongViewModel>(existingContent);
					model.ImageUrl = existingModel?.ImageUrl;
				}
			}

			var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
			var res = await _httpClient.PutAsync($"api/Phong/{id}", content);

			if (res.IsSuccessStatusCode) return RedirectToAction("Index");

			ModelState.AddModelError("", "Cập nhật thất bại.");
			model.LoaiPhongs = await GetLoaiPhongDropdown();
			return View(model);
		}

		// GET: Xoá
		public async Task<IActionResult> Delete(int id)
		{
			var res = await _httpClient.DeleteAsync($"api/Phong/{id}");
			return RedirectToAction("Index");
		}






		public async Task<IActionResult> Details(int id)
		{
			var phong = await _httpClient.GetFromJsonAsync<PhongViewModel>($"api/Phong/{id}");
			if (phong == null) return NotFound();

			return View(phong); // => hiển thị ra chi tiết phòng trong view
		}
		private async Task<List<SelectListItem>> GetLoaiPhongDropdown()
		{
			var res = await _httpClient.GetAsync("http://localhost:40841/api/loaiphong");
			if (!res.IsSuccessStatusCode) return new List<SelectListItem>();

			var json = await res.Content.ReadAsStringAsync();
			var loaiPhongs = JsonConvert.DeserializeObject<List<dynamic>>(json);

			return loaiPhongs.Select(x => new SelectListItem
			{
				Text = (string)x.tenLoai,
				Value = x.maLP.ToString()
			}).ToList();
		}


	}
}
