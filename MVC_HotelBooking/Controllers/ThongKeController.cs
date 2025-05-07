using Microsoft.AspNetCore.Mvc;
using MVC_HotelBooking.Models;

namespace MVC_HotelBooking.Controllers
{
    public class ThongKeController : Controller
    {
        private readonly HttpClient _httpClient;

        public ThongKeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7077/");
        }
        public IActionResult Index()
        {
            return View();
        }

        // Thống kê doanh thu
        public async Task<IActionResult> DoanhThu()
        {
            var response = await _httpClient.GetAsync("api/thongke/doanhthu-tong");

            if (response.IsSuccessStatusCode)
            {
                var doanhThu = await response.Content.ReadFromJsonAsync<TongDoanhThu>(); // Đọc kết quả từ API
                return View(doanhThu); // Truyền doanh thu vào View
            }
            return View("Error");
        }
        public async Task<IActionResult> DoanhThuTheoNgay()
        {
            var response = await _httpClient.GetAsync("api/thongke/doanhthu-ngay");

            if (response.IsSuccessStatusCode)
            {
                var doanhThu = await response.Content.ReadFromJsonAsync<List<DoanhThuTheoNgay>>();
                return View(doanhThu);
            }

            return View("Error");
        }
        public async Task<IActionResult> DoanhThuTheoThang()
        {
            var response = await _httpClient.GetAsync("api/thongke/doanhthu-thang");

            if (response.IsSuccessStatusCode)
            {
                var doanhThu = await response.Content.ReadFromJsonAsync<List<DoanhThuTheoThang>>();
                return View(doanhThu);
            }

            return View("Error");
        }
        public async Task<IActionResult> DoanhThuTheoNam()
        {
            var response = await _httpClient.GetAsync("api/thongke/doanhthu-nam");

            if (response.IsSuccessStatusCode)
            {
                var doanhThu = await response.Content.ReadFromJsonAsync<List<DoanhThuTheoNam>>();
                return View(doanhThu);
            }

            return View("Error");
        }
    }
}
