using Microsoft.AspNetCore.Mvc;
using MVC_HotelBooking.ViewModel;
using Newtonsoft.Json;

namespace MVC_HotelBooking.Controllers
{
    public class NguoiDungController : Controller
    {
        private readonly HttpClient _httpClient;

        public NguoiDungController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7077/api/");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("nguoidung/customers");
            if (!response.IsSuccessStatusCode)
                return View("Error");

            var json = await response.Content.ReadAsStringAsync();
            var danhSach = JsonConvert.DeserializeObject<List<NguoiDungViewModel>>(json);
            return View(danhSach);
        }
    }
}
