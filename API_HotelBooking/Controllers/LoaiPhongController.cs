
﻿using API_HotelBooking.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_HotelBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace API_HotelBooking.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class LoaiPhongController : ControllerBase
	{
		private readonly AppDbContext _context;

		public LoaiPhongController(AppDbContext context)
		{
			_context = context;
		}



		// GET: api/LoaiPhong
		[HttpGet]
		public async Task<ActionResult<IEnumerable<LoaiPhong>>> GetLoaiPhongs()
		{
			return await _context.LoaiPhongs.ToListAsync();
		}

		// GET: api/LoaiPhong/5
		[HttpGet("{id}")]
		public async Task<ActionResult<LoaiPhong>> GetLoaiPhong(int id)
		{
			var loaiPhong = await _context.LoaiPhongs.FindAsync(id);
			if (loaiPhong == null)
				return NotFound();
			return loaiPhong;
		}

		// POST: api/LoaiPhong
		[HttpPost]
		public async Task<ActionResult<LoaiPhong>> PostLoaiPhong(LoaiPhong loaiPhong)
		{
			_context.LoaiPhongs.Add(loaiPhong);
			await _context.SaveChangesAsync();
			return CreatedAtAction(nameof(GetLoaiPhong), new { id = loaiPhong.MaLP }, loaiPhong);
		}

		// PUT: api/LoaiPhong/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutLoaiPhong(int id, LoaiPhong loaiPhong)
		{
			if (id != loaiPhong.MaLP)
				return BadRequest();

			_context.Entry(loaiPhong).State = EntityState.Modified;
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_context.LoaiPhongs.Any(e => e.MaLP == id))
					return NotFound();
				throw;
			}

			return NoContent();
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteLoaiPhong(int id)
		{
			var loaiPhong = await _context.LoaiPhongs.FindAsync(id);
			if (loaiPhong == null)
				return NotFound();

			_context.LoaiPhongs.Remove(loaiPhong);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}



