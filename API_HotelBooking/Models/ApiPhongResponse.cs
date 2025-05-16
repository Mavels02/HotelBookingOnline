namespace API_HotelBooking.Models
{
    public class ApiPhongResponse

    {
        public IEnumerable<Phong> Rooms { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
