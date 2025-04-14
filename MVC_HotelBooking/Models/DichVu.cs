using System.ComponentModel.DataAnnotations;

namespace MVC_HotelBooking.Models
{
    public class DichVu
    {
        public int MaDV { get; set; }

        [Required]
        public string KieuDichVu { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Gia { get; set; }
    }
}
