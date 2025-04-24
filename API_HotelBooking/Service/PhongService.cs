using API_HotelBooking.Models;
using API_HotelBooking.ViewModels;
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

        public async Task<Phong> CreateAsync(Phong model)
        {
            try
            {
                _context.Phongs.Add(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                // In ra lỗi cụ thể
                Console.WriteLine("LỖI khi thêm phòng: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(int id, Phong model)
        {
            var existing = await _context.Phongs.FindAsync(id);
            if (existing == null) return false;

            existing.TenPhong = model.TenPhong;
            existing.GiaPhong = model.GiaPhong;
            existing.TrangThai = model.TrangThai;
            existing.MaLP = model.MaLP;
            existing.ImageUrl = model.ImageUrl;

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
        public async Task<(List<Phong> Rooms, int TotalPages)> GetPagedRoomsAsync(
       int page, int pageSize,
       int? loaiPhong = null, decimal? min = null,
       decimal? max = null, string? status = null)
        {
            var query = _context.Phongs.Include(p => p.LoaiPhong).AsQueryable();

            if (loaiPhong.HasValue)
                query = query.Where(p => p.MaLP == loaiPhong.Value);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(p => p.TrangThai.ToLower().Contains(status.ToLower()));

            if (min.HasValue)
                query = query.Where(p => p.GiaPhong >= min.Value);

            if (max.HasValue)
                query = query.Where(p => p.GiaPhong <= max.Value);

            var totalRooms = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalRooms / pageSize);

            var rooms = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (rooms, totalPages);
        }
    }
}
