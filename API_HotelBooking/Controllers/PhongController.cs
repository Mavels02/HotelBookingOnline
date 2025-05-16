using API_HotelBooking.Models;
using API_HotelBooking.Service;
using API_HotelBooking.DTOs;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions;

namespace API_HotelBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhongController : Controller
    {
        private readonly IPhongService _phongService;

        public PhongController(IPhongService phongService)
        {
            _phongService = phongService;

        }

        // GET: api/Phong
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Phong>>> GetAll()
        {
            var phongs = await _phongService.GetAllAsync();
            // Trả về danh sách phòng với LoaiPhongName thay vì LoaiPhong
            var phongViewModels = phongs.Select(p => new
            {
                p.MaP,
                p.MaLP,
                p.TenPhong,
                p.GiaPhong,
                p.TrangThai,
                 p.TenLoai, 
				p.SoLuongNguoiToiDa,
                p.ImageUrl
            }).ToList();

            return Ok(phongViewModels);
        }

		// GET: api/Phong/5
		[HttpGet("{id}")]
		public async Task<ActionResult<PhongViewModel>> GetById(int id)
		{
			var room = await _phongService.GetByIdAsync(id);
			if (room == null)
				return NotFound();

            // Trả về phòng với LoaiPhongName
            var phongViewModel = new
            {
				room.MaP,
				room.MaLP,
				room.TenPhong,
                room.GiaPhong,
				room.TrangThai,
				room.SoLuongNguoiToiDa,
                 room.TenLoai, // Lấy LoaiPhongName
				room.ImageUrl
            };

            return Ok(phongViewModel);
        }

		// POST: api/Phong
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] PhongViewModel model)
		{
			await _phongService.CreateAsync(model);
			return Ok(new { message = "Phòng đã được tạo thành công." });
		}

		// PUT: api/Phong/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] PhongViewModel model)
		{
			var success = await _phongService.UpdateAsync(id, model);
			if (!success)
				return NotFound();

			return NoContent();
		}

		// DELETE: api/Phong/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var success = await _phongService.DeleteAsync(id);
			if (!success)
				return NotFound();

			return NoContent();
		}

		[HttpGet("page")]
		public async Task<IActionResult> GetFiltered(
			int page = 1,
			int pageSize = 6,
			int? loaiPhong = null,
			decimal? min = null,
			decimal? max = null,
			string? status = null, string? search = null)
		{
			var (rooms, totalPages) = await _phongService.GetPagedRoomsAsync(
				page, pageSize, loaiPhong, min, max, status,search
			);

            var roomViewModels = rooms.Select(p => new PhongViewModel
            {
                MaP = p.MaP,
                TenPhong = p.TenPhong,
                GiaPhong = p.GiaPhong,
                TrangThai = p.TrangThai,
                ImageUrl = p.ImageUrl,
                TenLoai = p.LoaiPhong.TenLoai,
				SoLuongNguoiToiDa = p.SoLuongNguoiToiDa
			});

			var result = new ApiPhongResponse
			{
				Rooms = roomViewModels,
				CurrentPage = page,
				TotalPages = totalPages
			};

			return Ok(result);
		}
	}
}

