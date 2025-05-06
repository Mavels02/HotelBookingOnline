using API_HotelBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace API_HotelBooking.Service
{
    public class DatDichVuService : IDatDichVuService
    {
        private readonly AppDbContext _context;

        public DatDichVuService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DatDichVu>> GetAllAsync()
        {
            return await _context.DatDichVus
                                 .Include(x => x.DichVu)
                                 .ToListAsync();
        }

        public async Task<DatDichVu?> GetByIdAsync(int id)
        {
            return await _context.DatDichVus
                                 .Include(x => x.DichVu)
                                 .FirstOrDefaultAsync(x => x.MaDDV == id);
        }

        public async Task<DatDichVu> CreateAsync(DatDichVu entity)
        {
            // Kiểm tra xem DichVu có tồn tại không
            var dv = await _context.DichVus.FindAsync(entity.MaDV);
            if (dv == null)
            {
                throw new ArgumentException($"DichVu #{entity.MaDV} không tồn tại");
            }

            // Kiểm tra số lượng không được nhỏ hơn 1
            if (entity.SoLuong <= 0)
            {
                throw new ArgumentException("Số lượng phải lớn hơn 0");
            }

            // Tự tính TongTien nếu chưa có
            if (entity.TongTien == 0)
            {
                entity.TongTien = dv.Gia * entity.SoLuong;
            }

            // Tự động tính Ngay nếu không có
            if (entity.Ngay == default)
            {
                entity.Ngay = DateTime.Now;
            }

            // Thêm DatDichVu vào context và lưu vào DB
            _context.DatDichVus.Add(entity);
            await _context.SaveChangesAsync();

            // Load navigation property DichVu
            await _context.Entry(entity).Reference(x => x.DichVu).LoadAsync();

            return entity;
        }

        public async Task<bool> UpdateAsync(DatDichVu entity)
        {
            var existing = await _context.DatDichVus.FindAsync(entity.MaDDV);
            if (existing == null) return false;

            // Kiểm tra xem DichVu có tồn tại không
            var dv = await _context.DichVus.FindAsync(entity.MaDV)
                     ?? throw new ArgumentException($"DichVu #{entity.MaDV} không tồn tại");

            // Kiểm tra số lượng không được nhỏ hơn 1
            if (entity.SoLuong <= 0)
            {
                throw new ArgumentException("Số lượng phải lớn hơn 0");
            }

            existing.MaDV = entity.MaDV;
            existing.MaDP = entity.MaDP;
            existing.SoLuong = entity.SoLuong;
            existing.TongTien = entity.TongTien == 0
                                ? dv.Gia * entity.SoLuong
                                : entity.TongTien;
            existing.Ngay = entity.Ngay == default
                                ? existing.Ngay
                                : entity.Ngay;

            _context.Entry(existing).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.DatDichVus.FindAsync(id);
            if (entity == null) return false;

            _context.DatDichVus.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}