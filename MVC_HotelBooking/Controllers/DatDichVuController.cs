using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_HotelBooking.Models;
using MVC_HotelBooking.ViewModel;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MVC_HotelBooking.Controllers
{
    public class DatDichVuController : Controller
    {
        private const string BaseUrl = "https://localhost:7077/api/"; // URL API
        private const string Endpoint = "DatDichVu";

        public DatDichVuController() { }

        // Hiển thị danh sách đặt dịch vụ
        public async Task<IActionResult> Index()
        {
            using var client = new HttpClient { BaseAddress = new Uri(BaseUrl) };
            var list = await client.GetFromJsonAsync<IEnumerable<DatDichVu>>(Endpoint)
                           ?? Enumerable.Empty<DatDichVu>();
            return View(list);
        }

        // Form tạo mới đặt dịch vụ
        public async Task<IActionResult> Create(int maDV)
        {
            using var client = new HttpClient { BaseAddress = new Uri(BaseUrl) };

            // Lấy thông tin dịch vụ theo MaDV
            var service = await client.GetFromJsonAsync<DichVu>($"DichVu/{maDV}");
            if (service == null)
            {
                ModelState.AddModelError("", "Dịch vụ không tồn tại.");
                return RedirectToAction(nameof(Index));
            }

            // Tạo model với MaDV và giá dịch vụ
            var model = new DatDichVu
            {
                MaDV = maDV,
                TongTien = service.Gia // Tự động điền giá dịch vụ
            };

            return View(model);
        }

        // Xử lý khi người dùng gửi form tạo đặt dịch vụ
        [HttpPost]
        public async Task<IActionResult> Create(DatDichVu model)
        {
            if (ModelState.IsValid)
            {
                using var client = new HttpClient { BaseAddress = new Uri(BaseUrl) };

                // Lấy dịch vụ theo MaDV
                var service = await client.GetFromJsonAsync<DichVu>($"DichVu/{model.MaDV}");

                if (service != null)
                {
                    // Tính tổng tiền nếu người dùng chưa điền
                    model.TongTien = service.Gia * model.SoLuong;

                    // Gửi thông tin đặt dịch vụ đến API
                    var response = await client.PostAsJsonAsync(Endpoint, model);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "DichVu");
                    }

                    var errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"Lỗi từ API: {errorContent}");
                }
                else
                {
                    ModelState.AddModelError("", "Dịch vụ không tồn tại.");
                }
            }

            return View(model);
        }

        // Chỉnh sửa thông tin đặt dịch vụ
        public async Task<IActionResult> Edit(int id)
        {
            using var client = new HttpClient { BaseAddress = new Uri(BaseUrl) };
            var item = await client.GetFromJsonAsync<DatDichVu>($"{Endpoint}/{id}");
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DatDichVu model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using var client = new HttpClient { BaseAddress = new Uri(BaseUrl) };

            // Lấy lại thông tin dịch vụ để tính tổng tiền chính xác
            var service = await client.GetFromJsonAsync<DichVu>($"DichVu/{model.MaDV}");
            if (service != null)
            {
                model.TongTien = model.SoLuong * service.Gia;

                var response = await client.PutAsJsonAsync($"{Endpoint}/{model.MaDDV}", model);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index"); // hoặc "DatDichVu" nếu bạn muốn quay về danh sách đặt dịch vụ
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"Lỗi cập nhật từ API: {error}");
                }
            }
            else
            {
                ModelState.AddModelError("", "Không tìm thấy dịch vụ để tính giá.");
            }

            return View(model);
        }

        // Xóa thông tin đặt dịch vụ
        public async Task<IActionResult> Delete(int id)
        {
            using var client = new HttpClient { BaseAddress = new Uri(BaseUrl) };
            var item = await client.GetFromJsonAsync<DatDichVu>($"{Endpoint}/{id}");
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using var client = new HttpClient { BaseAddress = new Uri(BaseUrl) };
            await client.DeleteAsync($"{Endpoint}/{id}");
            return RedirectToAction("Index");
        }
    }
}