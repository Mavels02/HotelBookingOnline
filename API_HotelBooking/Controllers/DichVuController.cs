using API_HotelBooking.Models;
using API_HotelBooking.Service;
using Microsoft.AspNetCore.Mvc;

namespace API_HotelBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVuController : ControllerBase
    {
        private readonly IDichVuService _service;

        public DichVuController(IDichVuService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dichVus = await _service.GetAllAsync();
            return Ok(dichVus);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dichVu = await _service.GetByIdAsync(id);
            if (dichVu == null)
                return NotFound();
            return Ok(dichVu);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DichVu model)
        {
            var createdDichVu = await _service.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = createdDichVu.MaDV }, createdDichVu);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DichVu model)
        {
            var updated = await _service.UpdateAsync(id, model);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}