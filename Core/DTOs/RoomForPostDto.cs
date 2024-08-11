using Core.Enums;

namespace Core.DTOs
{
    public class RoomForPostDto
    {
        public int Id { get; set; }
        public RoomType Type { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChilds { get; set; }
        public bool IsBooked { get; set; }
        public int BranchID { get; set; }
        public int? BookingID { get; set; }
    }
}
