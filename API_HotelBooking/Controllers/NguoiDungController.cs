using API_HotelBooking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_HotelBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDungController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NguoiDungController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("customers")]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _context.NguoiDungs
                .Where(nd => nd.VaiTro.ToLower() == "Customer")
                .Select(nd => new
                {
                    nd.MaND,
                    nd.Ten,
                    nd.Email,
                    nd.SDT,
                    nd.VaiTro
                })
                .ToListAsync();

            return Ok(customers);
        }
    }
}