using API_HotelBooking.Models;
using API_HotelBooking.DTOs;
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

		public async Task<IEnumerable<PhongDTO>> GetAllAsync()
		{
			return await _context.Phongs.Include(p => p.LoaiPhong)
				.Select(p => new PhongDTO
				{
					MaP = p.MaP,
					TenPhong = p.TenPhong,
					GiaPhong = p.GiaPhong,
					TrangThai = p.TrangThai,
					ImageUrl = p.ImageUrl,
					SoLuongNguoiToiDa = p.SoLuongNguoiToiDa,
					MaLP = p.MaLP,
					TenLoai = p.LoaiPhong != null ? p.LoaiPhong.TenLoai : ""
				}).ToListAsync();
		}

		public async Task<PhongDTO> GetByIdAsync(int id)
		{
			var p = await _context.Phongs.Include(x => x.LoaiPhong).FirstOrDefaultAsync(p => p.MaP == id);
			if (p == null) return null;

			return new PhongDTO
			{
				MaP = p.MaP,
				TenPhong = p.TenPhong,
				GiaPhong = p.GiaPhong,
				TrangThai = p.TrangThai,
				ImageUrl = p.ImageUrl,
				SoLuongNguoiToiDa = p.SoLuongNguoiToiDa,
				MaLP = p.MaLP,
				TenLoai = p.LoaiPhong?.TenLoai
			};
		}

		public async Task CreateAsync(PhongDTO dto)
		{
			var p = new Phong
			{
				TenPhong = dto.TenPhong,
				GiaPhong = dto.GiaPhong,
				MoTa = dto.MoTa,
                TrangThai = dto.TrangThai,
				ImageUrl = dto.ImageUrl,
				SoLuongNguoiToiDa = dto.SoLuongNguoiToiDa,
				MaLP = dto.MaLP
			};
			_context.Phongs.Add(p);
			await _context.SaveChangesAsync();
		}

		public async Task<bool> UpdateAsync(int id, PhongDTO dto)
		{
			var p = await _context.Phongs.FindAsync(id);
			if (p == null) return false;

			p.TenPhong = dto.TenPhong;
			p.GiaPhong = dto.GiaPhong;
			p.MoTa = dto.MoTa;
            p.TrangThai = dto.TrangThai;
			p.ImageUrl = dto.ImageUrl;
			p.SoLuongNguoiToiDa = dto.SoLuongNguoiToiDa;
			p.MaLP = dto.MaLP;

			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var p = await _context.Phongs.FindAsync(id);
			if (p == null) return false;

			_context.Phongs.Remove(p);
			await _context.SaveChangesAsync();
			return true;
		}
		public async Task<(List<Phong> Rooms, int TotalPages)> GetPagedRoomsAsync(
       int page, int pageSize,
       int? loaiPhong = null, decimal? min = null,
       decimal? max = null, string? status = null, string? search = null)
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
			if (!string.IsNullOrEmpty(search))
				query = query.Where(p => p.TenPhong.Contains(search));

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
