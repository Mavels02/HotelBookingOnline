using API_HotelBooking.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_HotelBooking.Services
{
	public interface ILoaiPhongService
	{
        Task<IEnumerable<LoaiPhong>> GetLoaiPhongsAsync();
        Task<LoaiPhong> GetLoaiPhongAsync(int id);
        Task<LoaiPhong> CreateLoaiPhongAsync(LoaiPhong loaiPhong);
        Task<bool> UpdateLoaiPhongAsync(int id, LoaiPhong loaiPhong);
        Task<bool> DeleteLoaiPhongAsync(int id);
	}
}
