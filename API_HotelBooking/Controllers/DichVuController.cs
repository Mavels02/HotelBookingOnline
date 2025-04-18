using API_HotelBooking.Models;
using API_HotelBooking.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dv = await _service.GetByIdAsync(id);
            if (dv == null) return NotFound();
            return Ok(dv);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DichVu dichVu)
        {
            var created = await _service.CreateAsync(dichVu);
            return CreatedAtAction(nameof(GetById), new { id = created.MaDV }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DichVu dichVu)
        {
            var updated = await _service.UpdateAsync(id, dichVu);
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
