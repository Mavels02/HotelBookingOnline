using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_HotelBooking.Models;
using API_HotelBooking.Service;
using API_HotelBooking.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_HotelBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhuyenMaiController : ControllerBase
    {
        private readonly IKhuyenMaiService _khuyenMaiService;

        public KhuyenMaiController(IKhuyenMaiService khuyenMaiService)
        {
            _khuyenMaiService = khuyenMaiService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _khuyenMaiService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var km = await _khuyenMaiService.GetByIdAsync(id);
            if (km == null) return NotFound();
            return Ok(km);
        }

        [HttpPost]
        public async Task<IActionResult> Create(KhuyenMaiViewModel dto)
        {
            var result = await _khuyenMaiService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.MaKM }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, KhuyenMaiViewModel dto)
        {
            var success = await _khuyenMaiService.UpdateAsync(id, dto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _khuyenMaiService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}