using System.ComponentModel.DataAnnotations;

namespace MVC_HotelBooking.Models
{
    public class Phong
    {
        public int MaP { get; set; }

        [Required]
        public int MaLP { get; set; }

        [Required]
        [StringLength(100)]
        public string TenPhong { get; set; }

        [Required]
        public decimal GiaPhong { get; set; }

        [Required]
        [StringLength(50)]
        public string TrangThai { get; set; }

        public string? ImageUrl { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
	public class ApiResponse
	{
		public int TotalPages { get; set; }
		public int CurrentPage { get; set; }
		public List<Phong> Rooms { get; set; }
	}
}
