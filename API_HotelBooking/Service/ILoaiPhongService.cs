using API_HotelBooking.Models;
<<<<<<< HEAD
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
=======

namespace API_HotelBooking.Service
{
	public interface ILoaiPhongService
	{
		Task<IEnumerable<LoaiPhong>> GetAllAsync();
	}
>>>>>>> 77cef63b639585e4ccc394b4dd4fe8bd8701ebbd
}
