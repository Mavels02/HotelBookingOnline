using API_HotelBooking.Models;
using API_HotelBooking.Service;
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
		private readonly AppDbContext _context;
		public PhongController(IPhongService phongService,AppDbContext context)
        {
            _phongService = phongService;
            _context = context;
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
		[HttpGet]
		public IActionResult GetAll(int page = 1, int pageSize = 6)
		{
			var totalRooms = _context.Phongs.Count();
			var totalPages = (int)Math.Ceiling((double)totalRooms / pageSize);

			var pagedRooms = _context.Phongs
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToList();

			var result = new
			{
				Rooms = pagedRooms,
				CurrentPage = page,
				TotalPages = totalPages
			};

			return Ok(result);
		}

	}
}
