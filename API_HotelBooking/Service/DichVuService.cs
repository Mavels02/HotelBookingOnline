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

        public async Task<IEnumerable<DichVu>> GetAllAsync() => await _context.DichVus.ToListAsync();

        public async Task<DichVu> GetByIdAsync(int id) => await _context.DichVus.FindAsync(id);

        public async Task<DichVu> AddAsync(DichVu dichVu)
        {
            _context.DichVus.Add(dichVu);
            await _context.SaveChangesAsync();
            return dichVu;
        }

        public async Task UpdateAsync(DichVu dichVu)
        {
            _context.Entry(dichVu).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var dv = await _context.DichVus.FindAsync(id);
            if (dv != null)
            {
                _context.DichVus.Remove(dv);
                await _context.SaveChangesAsync();
            }
        }
    }
}
