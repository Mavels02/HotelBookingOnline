using Microsoft.AspNetCore.Mvc;
using MVC_HotelBooking.Models;
using Newtonsoft.Json;
using System.Text;

namespace MVC_HotelBooking.Controllers
{
    public class LoaiPhongController : Controller
    {
        private readonly HttpClient _httpClient;

        public LoaiPhongController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // Hiển thị danh sách loại phòng
        public async Task<IActionResult> Index()
        {
            var loaiPhongs = await _httpClient.GetFromJsonAsync<List<LoaiPhong>>("https://localhost:7077/api/LoaiPhong");
            return View(loaiPhongs);
        }

        // Tạo mới loại phòng
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LoaiPhong model)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7077/api/LoaiPhong", model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            var content = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Lỗi API: {content}");
            return View(model);
        }

        // Chỉnh sửa loại phòng
        public async Task<IActionResult> Edit(int id)
        {
            var loaiPhong = await _httpClient.GetFromJsonAsync<LoaiPhong>($"https://localhost:7077/api/LoaiPhong/{id}");
            if (loaiPhong == null)
                return NotFound();
            return View(loaiPhong);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LoaiPhong model)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7077/api/LoaiPhong/{model.MaLP}", model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            return View(model);
        }

        // Xóa loại phòng
        public async Task<IActionResult> Delete(int id)
        {
            var loaiPhong = await _httpClient.GetFromJsonAsync<LoaiPhong>($"https://localhost:7077/api/LoaiPhong/{id}");
            if (loaiPhong == null)
                return NotFound();
            return View(loaiPhong);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7077/api/LoaiPhong/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            return View();
        }
    }
}
