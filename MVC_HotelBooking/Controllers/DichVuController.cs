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

        public DichVuController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            var dichVus = await _httpClient.GetFromJsonAsync<List<DichVu>>("https://localhost:7077/api/DichVu");
            return View(dichVus);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DichVu model)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7077/api/DichVu", model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(DichVu model)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7077/api/DichVu/{model.MaDV}", model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(Index));
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var dichVu = await _httpClient.GetFromJsonAsync<DichVu>($"https://localhost:7077/api/DichVu/{id}");
            if (dichVu == null)
                return NotFound();
            return View(dichVu);
        }
    }
}
