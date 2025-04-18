using API_HotelBooking.Models;
using API_HotelBooking.Service;
using Microsoft.AspNetCore.Mvc;

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
    }
}
