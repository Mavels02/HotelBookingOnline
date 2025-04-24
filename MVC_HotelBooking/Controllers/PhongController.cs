using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_HotelBooking.Models;

using MVC_HotelBooking.ViewModel;
using Refit;
using System.Text.Json;

using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;


namespace MVC_HotelBooking.Controllers
{
    public class PhongController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _env;
        private readonly string apiBaseUrl = "https://localhost:7077/api";

        public PhongController(HttpClient httpClient, IWebHostEnvironment env)
        {
            _httpClient = httpClient;
            _env = env;
        }

        private async Task<List<SelectListItem>> GetLoaiPhongDropdown()
        {
            var loaiPhongDropdown = new List<SelectListItem>();

            // Gọi API lấy danh sách loại phòng
            var res = await _httpClient.GetAsync($"{apiBaseUrl}/LoaiPhong");

            if (!res.IsSuccessStatusCode)
            {
                // Log lỗi nếu API không thành công
                Console.WriteLine("Lỗi gọi API LoaiPhong: " + res.StatusCode);
                return loaiPhongDropdown;
            }

            // Đọc nội dung phản hồi từ API
            var json = await res.Content.ReadAsStringAsync();

            Console.WriteLine("JSON từ API LoaiPhong: " + json); // Kiểm tra xem JSON có đúng không

            try
            {
                // Kiểm tra nếu JSON là hợp lệ và deserialize thành danh sách LoaiPhong
                var loaiPhongs = JsonConvert.DeserializeObject<List<LoaiPhong>>(json);

                if (loaiPhongs != null && loaiPhongs.Any())
                {
                    // Chuyển đổi LoaiPhong thành SelectListItem để sử dụng trong dropdown
                    loaiPhongDropdown = loaiPhongs.Select(lp => new SelectListItem
                    {
                        Value = lp.MaLP.ToString(),
                        Text = lp.LoaiPhongName
                    }).ToList();
                }
                else
                {
                    Console.WriteLine("Không có loại phòng nào trong dữ liệu JSON.");
                }
            }
            catch (Exception ex)
            {
                // Log lỗi nếu gặp vấn đề khi deserialize JSON
                Console.WriteLine("Lỗi khi parse JSON: " + ex.Message);
            }

            return loaiPhongDropdown;
        }

        public async Task<IActionResult> Index()
        {
            var res = await _httpClient.GetAsync($"{apiBaseUrl}/Phong");
            if (!res.IsSuccessStatusCode)
                return View(new List<Phong>());

            var json = await res.Content.ReadAsStringAsync();
            var phongs = JsonConvert.DeserializeObject<List<Phong>>(json);
            return View(phongs);
        }

        public async Task<IActionResult> Create()
        {
            var model = new Phong
            {
                LoaiPhongs = await GetLoaiPhongDropdown()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Phong model)
        {
            if (!ModelState.IsValid)
            {
                // Nếu dữ liệu không hợp lệ, gọi lại API để lấy loại phòng
                model.LoaiPhongs = await GetLoaiPhongDropdown();
                return View(model);
            }

            // Kiểm tra ảnh nếu có file ảnh
            if (model.ImageFile != null)
            {
                // Kiểm tra loại file ảnh
                if (!IsValidImage(model.ImageFile))
                {
                    model.LoaiPhongs = await GetLoaiPhongDropdown();
                    ModelState.AddModelError(string.Empty, "File ảnh không hợp lệ.");
                    return View(model);
                }

                var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
                var path = Path.Combine(_env.WebRootPath, "image", fileName);

                // Lưu ảnh vào thư mục
                using var stream = new FileStream(path, FileMode.Create);
                await model.ImageFile.CopyToAsync(stream);

                model.ImageUrl = "/image/" + fileName;
            }

            // Chuẩn bị dữ liệu gửi tới API
            var apiModel = new
            {
                model.MaLP,
                model.TenPhong,
                model.GiaPhong,
                model.TrangThai,
                model.ImageUrl
            };

            // Chuyển thành JSON
            var json = JsonConvert.SerializeObject(apiModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Gửi POST request tới API
            var response = await _httpClient.PostAsync($"{apiBaseUrl}/Phong", content);

            if (!response.IsSuccessStatusCode)
            {


                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Lỗi khi gọi API tạo phòng: " + errorMessage);

                // Cập nhật lại dropdown
                model.LoaiPhongs = await GetLoaiPhongDropdown();

                // Thêm thông báo lỗi
                ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi khi thêm phòng. Chi tiết: " + errorMessage);
                return View(model);

            }

            // Nếu thành công, chuyển hướng đến danh sách phòng
            return RedirectToAction(nameof(Index));
        }

        // Hàm kiểm tra file ảnh
        private bool IsValidImage(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            return allowedExtensions.Contains(fileExtension);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var res = await _httpClient.GetAsync($"{apiBaseUrl}/Phong/{id}");
            if (!res.IsSuccessStatusCode)
                return NotFound();

            var json = await res.Content.ReadAsStringAsync();
            var phong = JsonConvert.DeserializeObject<Phong>(json);
            phong.LoaiPhongs = await GetLoaiPhongDropdown();
            return View(phong);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Phong model)
        {
            if (!ModelState.IsValid)
            {
                model.LoaiPhongs = await GetLoaiPhongDropdown();
                return View(model);
            }

            if (model.ImageFile != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
                var path = Path.Combine(_env.WebRootPath, "image", fileName);
                using var stream = new FileStream(path, FileMode.Create);
                await model.ImageFile.CopyToAsync(stream);
                model.ImageUrl = "/image/" + fileName;
            }

            var apiModel = new
            {
                model.MaLP,
                model.TenPhong,
                model.GiaPhong,
                model.TrangThai,
                model.ImageUrl
            };

            var json = JsonConvert.SerializeObject(apiModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"{apiBaseUrl}/Phong/{id}", content);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var res = await _httpClient.GetAsync($"{apiBaseUrl}/Phong/{id}");
            if (!res.IsSuccessStatusCode)
                return NotFound();

            var json = await res.Content.ReadAsStringAsync();
            var phong = JsonConvert.DeserializeObject<Phong>(json);
            return View(phong);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var res = await _httpClient.DeleteAsync($"{apiBaseUrl}/Phong/{id}");

            if (!res.IsSuccessStatusCode)
            {
                // Nếu có lỗi xảy ra khi xóa, bạn có thể thông báo lỗi cho người dùng
                var errorMessage = await res.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, "Lỗi khi xóa phòng: " + errorMessage);
                return RedirectToAction(nameof(Index));
            }

            // Sau khi xóa thành công, chuyển hướng về danh sách phòng
            return RedirectToAction(nameof(Index));
        }

		public async Task<IActionResult> Details(int id)
		{
			var phong = await _httpClient.GetFromJsonAsync<PhongViewModel>($"api/Phong/{id}");
			if (phong == null) return NotFound();

			return View(phong); // => hiển thị ra chi tiết phòng trong view
		}




    }
}
