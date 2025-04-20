using API_HotelBooking.Models;
using API_HotelBooking.Service;
using API_HotelBooking.ViewModels;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _phongService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _phongService.GetByIdAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Phong phong)
        {
            if (ModelState.IsValid)
            {
                var created = await _phongService.CreateAsync(phong);
                return CreatedAtAction(nameof(GetById), new { id = created.MaP }, created);
            }

            return BadRequest(ModelState);  // Trả về lỗi nếu Model không hợp lệ
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Phong phong)
        {
            var result = await _phongService.UpdateAsync(id, phong);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _phongService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
		[HttpGet("page")]
		public async Task<IActionResult> GetFiltered(
			int page = 1,
			int pageSize = 6,
			int? loaiPhong = null,
			decimal? min = null,
			decimal? max = null,
			string? status = null)
		{
			var (rooms, totalPages) = await _phongService.GetPagedRoomsAsync(
				page, pageSize, loaiPhong, min, max, status
			);

            var roomViewModels = rooms.Select(p => new PhongViewModel
            {
                MaP = p.MaP,
                TenPhong = p.TenPhong,
                GiaPhong = p.GiaPhong,
                TrangThai = p.TrangThai,
                ImageUrl = p.ImageUrl,
                LoaiPhongName = p.LoaiPhong.LoaiPhongName
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

