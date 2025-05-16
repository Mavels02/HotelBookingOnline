using API_HotelBooking.Models;
using API_HotelBooking.DTOs;
using Microsoft.EntityFrameworkCore;

namespace API_HotelBooking.Service
{
    public class KhuyenMaiService : IKhuyenMaiService
    {
        private readonly AppDbContext _context;

        public KhuyenMaiService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<KhuyenMaiDTO>> GetAllAsync()
        {
            return await _context.KhuyenMais
                .Select(km => new KhuyenMaiDTO
                {
                    MaKM = km.MaKM,
                    TenKM = km.TenKM,
                    PhanTramKM = km.PhanTramKM,
                    NgayBatDau = km.NgayBatDau,
                    NgayKetThuc = km.NgayKetThuc,
                    TrangThai = km.TrangThai
                }).ToListAsync();
        }

        public async Task<KhuyenMaiDTO> GetByIdAsync(int id)
        {
            var km = await _context.KhuyenMais.FindAsync(id);
            if (km == null) return null;

            return new KhuyenMaiDTO
            {
                MaKM = km.MaKM,
                TenKM = km.TenKM,
                PhanTramKM = km.PhanTramKM,
                NgayBatDau = km.NgayBatDau,
                NgayKetThuc = km.NgayKetThuc,
                TrangThai = km.TrangThai
            };
        }

        public async Task<KhuyenMaiDTO> CreateAsync(KhuyenMaiDTO dto)
        {
            var km = new KhuyenMai
            {
                TenKM = dto.TenKM,
                PhanTramKM = dto.PhanTramKM,
                NgayBatDau = dto.NgayBatDau,
                NgayKetThuc = dto.NgayKetThuc,
                TrangThai = dto.TrangThai
            };

            _context.KhuyenMais.Add(km);
            await _context.SaveChangesAsync();

            dto.MaKM = km.MaKM;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, KhuyenMaiDTO dto)
        {
            var km = await _context.KhuyenMais.FindAsync(id);
            if (km == null) return false;

            km.TenKM = dto.TenKM;
            km.PhanTramKM = dto.PhanTramKM;
            km.NgayBatDau = dto.NgayBatDau;
            km.NgayKetThuc = dto.NgayKetThuc;
            km.TrangThai = dto.TrangThai;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var km = await _context.KhuyenMais.FindAsync(id);
            if (km == null) return false;

            _context.KhuyenMais.Remove(km);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}