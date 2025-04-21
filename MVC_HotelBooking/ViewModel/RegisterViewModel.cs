using System.ComponentModel.DataAnnotations;

namespace MVC_HotelBooking.ViewModel
{
	public class RegisterViewModel
	{
		public string Ten { get; set; }
		public string Email { get; set; }
		public string SDT { get; set; }
		public string MatKhau { get; set; }
		public string ConfirmPassword { get; set; }
	}
}
