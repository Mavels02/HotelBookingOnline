using API_HotelBooking.Models;
using API_HotelBooking.DTOs;

namespace API_HotelBooking.Service
{
    public interface IKhuyenMaiService
    {
        Task<IEnumerable<KhuyenMaiDTO>> GetAllAsync();
        Task<KhuyenMaiDTO> GetByIdAsync(int id);
        Task<KhuyenMaiDTO> CreateAsync(KhuyenMaiDTO dto);
        Task<bool> UpdateAsync(int id, KhuyenMaiDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}