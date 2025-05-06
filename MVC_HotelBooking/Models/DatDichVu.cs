using System.ComponentModel.DataAnnotations;

namespace MVC_HotelBooking.Models
{
    public class DatDichVu
    {
        public int MaDDV { get; set; } // Khóa chính

        [Required]
        public int MaDV { get; set; } // Mã dịch vụ

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int SoLuong { get; set; }

        [DataType(DataType.Currency)]
        public decimal TongTien { get; set; } // Nếu để 0 sẽ tự tính trên server

        [DataType(DataType.Date)]
        public DateTime Ngay { get; set; } = DateTime.Now; // Mặc định ngày hiện tại

        // Tùy chọn thêm trường MaDP nếu cần
        public int? MaDP { get; set; }
    }
}