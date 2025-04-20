using API_HotelBooking.Models;
using API_HotelBooking.ViewModels;
using Microsoft.EntityFrameworkCore;
using API_HotelBooking.Service;

namespace API_HotelBooking.Service
{
	public class LoaiPhongService : ILoaiPhongService
	{
		private readonly AppDbContext _context;

		public LoaiPhongService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<LoaiPhong>> GetAllAsync()
		{
			return await _context.LoaiPhongs.ToListAsync();
		}
	}
}