using API_HotelBooking.Models;
using API_HotelBooking.Service;
using Microsoft.AspNetCore.Mvc;

namespace API_HotelBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVuController : Controller
    {
        private readonly IDichVuService _service;

        public DichVuController(IDichVuService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DichVu>>> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<DichVu>> GetById(int id)
        {
            var dv = await _service.GetByIdAsync(id);
            if (dv == null) return NotFound();
            return Ok(dv);
        }

        [HttpPost]
        public async Task<ActionResult<DichVu>> Create(DichVu dichVu)
        {
            var created = await _service.AddAsync(dichVu);
            return CreatedAtAction(nameof(GetById), new { id = created.MaDV }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DichVu dichVu)
        {
            if (id != dichVu.MaDV) return BadRequest();
            await _service.UpdateAsync(dichVu);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
