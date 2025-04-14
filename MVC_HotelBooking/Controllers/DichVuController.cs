using Microsoft.AspNetCore.Mvc;
using MVC_HotelBooking.Models;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace MVC_HotelBooking.Controllers
{
    public class DichVuController : Controller
    {
        private readonly HttpClient _httpClient;

        public DichVuController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:7165/api/dichvu");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<IEnumerable<DichVu>>(json);
                return View(data);
            }
            return View(new List<DichVu>());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(DichVu dichVu)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7165/api/dichvu", dichVu);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Lỗi khi tạo dịch vụ: {errorContent}");
            return View(dichVu);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7165/api/dichvu/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var dv = JsonConvert.DeserializeObject<DichVu>(json);
                return View(dv);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DichVu dichVu)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7165/api/dichvu/{dichVu.MaDV}", dichVu);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Không thể cập nhật dịch vụ.");
            return View(dichVu);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7165/api/dichvu/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var dv = JsonConvert.DeserializeObject<DichVu>(json);
                return View(dv);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7165/api/dichvu/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View("Error");
        }
    }
}
