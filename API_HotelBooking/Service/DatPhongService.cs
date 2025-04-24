using API_HotelBooking.Models;
using API_HotelBooking.ViewModels;
using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace API_HotelBooking.Service
{
	public class DatPhongService: IDatPhongService
	{
		private readonly AppDbContext _context;

		public DatPhongService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<DatPhong> DatPhongAsync(DatPhongViewModel dto)
		{
			var phong = await _context.Phongs.FindAsync(dto.MaP);
			if (phong == null || phong.TrangThai == "Đang thuê" || phong.TrangThai == "Đang chờ thanh toán")
				throw new Exception("Phòng không khả dụng.");

			if (dto.SoNguoi > phong.SoLuongNguoiToiDa)
				throw new Exception("Vượt quá số người tối đa.");

			var soNgay = (dto.ThoiGianCheckOut - dto.ThoiGianCheckIn).Days;
			if (soNgay <= 0)
				throw new Exception("Ngày check-in/check-out không hợp lệ.");

			decimal tongTien = soNgay * phong.GiaPhong;

			if (dto.MaKM.HasValue)
			{
				var km = await _context.KhuyenMais.FindAsync(dto.MaKM.Value);
				if (km != null)
					tongTien *= (1 - ((decimal)km.PhanTramKM / 100));
			}

			var datPhong = new DatPhong
			{
				MaND = dto.MaND,
				MaP = dto.MaP,
				MaKM = dto.MaKM,
				ThoiGianDat = DateTime.Now,
				ThoiGianCheckIn = dto.ThoiGianCheckIn,
				ThoiGianCheckOut = dto.ThoiGianCheckOut,
				SoNguoi = dto.SoNguoi,
				TrangThai = "Đang chờ thanh toán",
				TongTien = tongTien
			};

			phong.TrangThai = "Đang chờ thanh toán";

			_context.DatPhongs.Add(datPhong);
			await _context.SaveChangesAsync();

			return datPhong;
		}

		public async Task<IEnumerable<DatPhong>> GetAllAsync()
		{
			return await _context.DatPhongs
				.Include(dp => dp.Phong)
				.Include(dp => dp.NguoiDung)
				.ToListAsync();
		}

		public async Task<DatPhongViewModel> GetThongTinDatPhongAsync(int MaPhong)
		{
			var phong = await _context.Phongs.FindAsync(MaPhong);
			if (phong == null)
				throw new Exception("Phòng không tồn tại");

			var viewModel = new DatPhongViewModel
			{
				MaP = MaPhong,
				TenPhong = phong.TenPhong,
				ThoiGianCheckIn = DateTime.Today,
				ThoiGianCheckOut = DateTime.Today.AddDays(1),
				SoNguoi = 1
			};

			return viewModel;
		}

		public async Task<bool> HuyDatPhongAsync(int id)
		{
			var dp = await _context.DatPhongs.FindAsync(id);
			if (dp == null) return false;

			var phong = await _context.Phongs.FindAsync(dp.MaP);
			if (phong != null) phong.TrangThai = "Trống";

			_context.DatPhongs.Remove(dp);
			await _context.SaveChangesAsync();

			return true;
		}
	}
}
