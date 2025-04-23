using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_HotelBooking.Models
{
    public class DatPhong
    {
        [Key]
        public int MaDP { get; set; }

        public int MaND { get; set; }
        [ForeignKey("MaND")]
        public NguoiDung NguoiDung { get; set; }

        public int MaP { get; set; }
        [ForeignKey("MaP")]
        public Phong Phong { get; set; }

        public int? MaKM { get; set; }
        [ForeignKey("MaKM")]
        public KhuyenMai KhuyenMai { get; set; }

        public DateTime ThoiGianDat { get; set; }
        public DateTime ThoiGianCheckIn { get; set; }
        public DateTime ThoiGianCheckOut { get; set; }
        public int SoNguoi {  get; set; }
        public string TrangThai { get; set; }
        public decimal TongTien { get; set; }

        public ICollection<DatDichVu> DatDichVus { get; set; }
        public ThanhToan ThanhToan { get; set; }
    }
}
