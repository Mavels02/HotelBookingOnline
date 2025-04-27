using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MVC_HotelBooking.ViewModel
{
	public class PhongViewModel
	{
		public int MaP { get; set; }
		public int? MaLP { get; set; }

		[Required]
		public string TenPhong { get; set; }

		[Required]
		public decimal GiaPhong { get; set; }

		[Required]
		public string TrangThai { get; set; }

		public string? ImageUrl { get; set; }

		[Required]
		public int SoLuongNguoiToiDa { get; set; }

		public string? TenLoai { get; set; }
		public IFormFile? ImageFile { get; set; }
		public List<SelectListItem>? LoaiPhongs { get; set; }
		

	}
}
