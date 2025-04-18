using Microsoft.AspNetCore.Mvc;
using MVC_HotelBooking.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace MVC_HotelBooking.Controllers
{
    public class PhongController : Controller
    {
        private readonly HttpClient _httpClient;

        public PhongController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7165/api/");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Phong");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<List<Phong>>();
                return View(data);
            }

            return View(new List<Phong>());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Phong phong)
        {
            if (!ModelState.IsValid)
            {
                // Kiểm tra và in ra các lỗi validation nếu có
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);  // In lỗi ra console (hoặc có thể ghi lại log)
                }
                return View(phong);
            }

            // Nếu có ảnh, xử lý upload ảnh
            if (phong.ImageFile != null)
            {
                // Xử lý lưu ảnh (ví dụ: lưu vào thư mục "wwwroot/images")
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", phong.ImageFile.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await phong.ImageFile.CopyToAsync(stream);
                }

                // Cập nhật đường dẫn hình ảnh vào model
                phong.ImageUrl = "/images/" + phong.ImageFile.FileName;
            }

            var response = await _httpClient.PostAsJsonAsync("Phong", phong);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Error: " + errorContent);  // Xem thông tin lỗi từ API
                return View(phong);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"Phong/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var phong = await response.Content.ReadFromJsonAsync<Phong>();
            return View(phong);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Phong phong)
        {
            if (!ModelState.IsValid)
                return View(phong);

            var response = await _httpClient.PutAsJsonAsync($"Phong/{id}", phong);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(phong);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"Phong/{id}");
            return RedirectToAction("Index");
        }
    }
}
