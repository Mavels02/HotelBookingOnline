using API_HotelBooking.Models;
using API_HotelBooking.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_HotelBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatDichVuController : ControllerBase
    {
        private readonly IDatDichVuService _svc;
        public DatDichVuController(IDatDichVuService svc)
        {
            _svc = svc;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DatDichVu>>> GetAll()
            => Ok(await _svc.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DatDichVu>> GetById(int id)
        {
            var item = await _svc.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<DatDichVu>> Create(DatDichVu input)
        {
            var created = await _svc.CreateAsync(input);
            return CreatedAtAction(nameof(GetById), new { id = created.MaDDV }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, DatDichVu input)
        {
            if (id != input.MaDDV)
                return BadRequest("ID không khớp");
            var ok = await _svc.UpdateAsync(input);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _svc.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}