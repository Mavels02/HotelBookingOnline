using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_HotelBooking.Models
{
    public class Phong
    {
        [Key]
        public int MaP { get; set; }

        [Required]
        public int? MaLP { get; set; }

        [ForeignKey("MaLP")]
        public LoaiPhong? LoaiPhong { get; set; }

        [Required]
        [StringLength(100)]
        public string TenPhong { get; set; }  //Tên phòng

        public decimal GiaPhong { get; set; }
        public string MoTa {  get; set; }

        public string TrangThai { get; set; }

        public string? ImageUrl { get; set; } 
        public int SoLuongNguoiToiDa { get; set; }



        public ICollection<DatPhong>? DatPhongs { get; set; }
    }
}
