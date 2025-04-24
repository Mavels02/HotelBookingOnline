using API_HotelBooking.Models;
using API_HotelBooking.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_HotelBooking.Services
{
	public class LoaiPhongService : ILoaiPhongService
	{
		private readonly AppDbContext _context;

		public LoaiPhongService(AppDbContext context)
		{
			_context = context;
		}

        public async Task<IEnumerable<LoaiPhong>> GetLoaiPhongsAsync()
        {
            return await _context.LoaiPhongs.Include(lp => lp.Phongs).ToListAsync();
        }

        public async Task<LoaiPhong> GetLoaiPhongAsync(int id)
        {
            return await _context.LoaiPhongs.Include(lp => lp.Phongs)
                                             .FirstOrDefaultAsync(lp => lp.MaLP == id);
        }

        public async Task<LoaiPhong> CreateLoaiPhongAsync(LoaiPhong loaiPhong)
        {
            _context.LoaiPhongs.Add(loaiPhong);
            await _context.SaveChangesAsync();
            return loaiPhong;
        }

        public async Task<bool> UpdateLoaiPhongAsync(int id, LoaiPhong loaiPhong)
        {
            var existingLoaiPhong = await _context.LoaiPhongs.FindAsync(id);
            if (existingLoaiPhong == null)
                return false;

            existingLoaiPhong.LoaiPhongName = loaiPhong.LoaiPhongName;

            _context.Entry(existingLoaiPhong).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteLoaiPhongAsync(int id)
		{
            var loaiPhong = await _context.LoaiPhongs.FindAsync(id);
            if (loaiPhong == null)
                return false;

            _context.LoaiPhongs.Remove(loaiPhong);
            await _context.SaveChangesAsync();
            return true;
		}
	}
}