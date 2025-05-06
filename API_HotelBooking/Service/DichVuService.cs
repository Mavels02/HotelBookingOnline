using API_HotelBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace API_HotelBooking.Service
{
    public class DichVuService : IDichVuService
    {
        private readonly AppDbContext _context;

        public DichVuService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DichVu>> GetAllAsync()
        {
            return await _context.DichVus.ToListAsync();
        }

        public async Task<DichVu> GetByIdAsync(int id)
        {
            return await _context.DichVus.FindAsync(id);
        }

        public async Task<DichVu> CreateAsync(DichVu dichVu)
        {
            _context.DichVus.Add(dichVu);
            await _context.SaveChangesAsync();
            return dichVu;
        }

        public async Task<bool> UpdateAsync(int id, DichVu dichVu)
        {
            var existing = await _context.DichVus.FindAsync(id);
            if (existing == null) return false;

            existing.KieuDichVu = dichVu.KieuDichVu;
            existing.Gia = dichVu.Gia;
            existing.ImageUrl = dichVu.ImageUrl; // Lưu đường dẫn ảnh

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var dichVu = await _context.DichVus.FindAsync(id);
            if (dichVu == null) return false;

            _context.DichVus.Remove(dichVu);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}