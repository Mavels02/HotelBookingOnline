using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using MVC_HotelBooking.ViewModel;
namespace MVC_HotelBooking.Controllers
{
    public class KhuyenMaiController : Controller
    {
        private readonly HttpClient _httpClient;

        public KhuyenMaiController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7077/api/");
        }


        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("khuyenmai");
            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var content = await response.Content.ReadAsStringAsync();
            var danhSachKM = JsonConvert.DeserializeObject<List<KhuyenMaiViewModel>>(content);
            return View(danhSachKM);
        }

        public IActionResult Create() => View();


        [HttpPost]
        public async Task<IActionResult> Create(KhuyenMaiViewModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("khuyenmai", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"khuyenmai/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var data = await response.Content.ReadAsStringAsync();
            var khuyenMai = JsonConvert.DeserializeObject<KhuyenMaiViewModel>(data);
            return View(khuyenMai);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, KhuyenMaiViewModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"khuyenmai/{id}", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"khuyenmai/{id}");
            return RedirectToAction("Index");
        }
    }
}
