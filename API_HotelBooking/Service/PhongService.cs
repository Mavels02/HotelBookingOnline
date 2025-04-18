using API_HotelBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace API_HotelBooking.Service
{
    public class PhongService : IPhongService
    {
        private readonly AppDbContext _context;
        public PhongService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Phong>> GetAllAsync()
        {
            return await _context.Phongs.Include(p => p.LoaiPhong).ToListAsync();
        }

        public async Task<Phong?> GetByIdAsync(int id)
        {
            return await _context.Phongs.Include(p => p.LoaiPhong).FirstOrDefaultAsync(p => p.MaP == id);
        }

        public async Task<Phong> CreateAsync(Phong phong)
        {
            _context.Phongs.Add(phong);
            await _context.SaveChangesAsync();
            return phong;
        }

        public async Task<bool> UpdateAsync(int id, Phong phong)
        {
            var existing = await _context.Phongs.FindAsync(id);
            if (existing == null) return false;

            existing.TenPhong = phong.TenPhong;
            existing.GiaPhong = phong.GiaPhong;
            existing.TrangThai = phong.TrangThai;
            existing.MaLP = phong.MaLP;
            existing.ImageUrl = phong.ImageUrl;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var phong = await _context.Phongs.FindAsync(id);
            if (phong == null) return false;

            _context.Phongs.Remove(phong);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
