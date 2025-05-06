using Microsoft.AspNetCore.Mvc;
using MVC_HotelBooking.Models;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using System.Text.Json;
namespace MVC_HotelBooking.Controllers
{
    public class DichVuController : Controller
    {
        private readonly HttpClient _httpClient;

        public DichVuController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7077/");
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var dichVus = await _httpClient.GetFromJsonAsync<List<DichVu>>("api/DichVu") ?? new List<DichVu>();

                if (User.IsInRole("Admin"))
                    return View("IndexAdmin", dichVus);

                return View("Index", dichVus);
            }
            catch (Exception ex)
            {
                return Content($"Lỗi: {ex.Message}");
            }
        }
        public async Task<IActionResult> IndexAdmin()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/DichVu");
                if (response.IsSuccessStatusCode)
                {
                    var dichVus = await response.Content.ReadFromJsonAsync<List<DichVu>>() ?? new List<DichVu>();
                    return View(dichVus); // View mặc định sẽ là IndexAdmin.cshtml
                }
                else
                {
                    return Content($"API trả về lỗi: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                return Content($"Lỗi khi gọi API: {ex.Message}");
            }
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DichVu model, IFormFile? imageFile)
        {
            if (imageFile != null)
            {
                // Save the image and get the URL
                var imageUrl = await SaveImageAsync(imageFile);
                model.ImageUrl = imageUrl;
            }

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7077/api/DichVu", model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(IndexAdmin));

            var content = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Lỗi API: {content}");
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dichVu = await _httpClient.GetFromJsonAsync<DichVu>($"https://localhost:7077/api/DichVu/{id}");
            if (dichVu == null)
                return NotFound();
            return View(dichVu);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DichVu model, IFormFile? imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var imageUrl = await SaveImageAsync(imageFile);
                model.ImageUrl = imageUrl;
            }

            // Nếu không chọn lại ảnh, giữ nguyên ảnh cũ (nếu có)
            if (string.IsNullOrEmpty(model.ImageUrl))
            {
                var existing = await _httpClient.GetFromJsonAsync<DichVu>($"https://localhost:7077/api/DichVu/{model.MaDV}");
                if (existing != null)
                {
                    model.ImageUrl = existing.ImageUrl;
                }
            }

            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7077/api/DichVu/{model.MaDV}", model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(IndexAdmin));

            var content = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Lỗi API: {content}");

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dichVu = await _httpClient.GetFromJsonAsync<DichVu>($"https://localhost:7077/api/DichVu/{id}");
            if (dichVu == null)
                return NotFound();
            return View(dichVu);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7077/api/DichVu/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(IndexAdmin));

            return View();
        }

        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            // Lưu file ảnh và trả về URL (ví dụ: "images/filename.jpg")
            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            var filePath = Path.Combine(uploads, imageFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Giả sử ảnh được lưu trong thư mục wwwroot/images
            return $"/images/{imageFile.FileName}";
        }
    }
}