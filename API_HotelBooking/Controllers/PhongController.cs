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
                LoaiPhongName = p.LoaiPhong.LoaiPhongName, // Trả về LoaiPhongName từ bảng LoaiPhong
                p.ImageUrl
            }).ToList();

            return Ok(phongViewModels);
        }

        // GET: api/Phong/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Phong>> GetById(int id)
        {
            var phong = await _phongService.GetByIdAsync(id);
            if (phong == null)
            {
                return NotFound();
            }

            // Trả về phòng với LoaiPhongName
            var phongViewModel = new
            {
                phong.MaP,
                phong.MaLP,
                phong.TenPhong,
                phong.GiaPhong,
                phong.TrangThai,
                LoaiPhongName = phong.LoaiPhong.LoaiPhongName, // Lấy LoaiPhongName
                phong.ImageUrl
            };

            return Ok(phongViewModel);
        }

        // POST: api/Phong
        [HttpPost]
        public async Task<ActionResult<Phong>> Create(Phong model)
        {
            var createdPhong = await _phongService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = createdPhong.MaP }, createdPhong);
        }

        // PUT: api/Phong/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Phong model)
        {
            var success = await _phongService.UpdateAsync(id, model);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/Phong/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _phongService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
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

