using System.ComponentModel.DataAnnotations;
namespace Core.DTOs
{
    public class BookingForPostDto
    {
        [Required]
        public DateTime CheckIn { get; set; } = DateTime.Now;

        [Required]
        public DateTime CheckOut { get; set; } = DateTime.Now;

        public string? userID { get; set; }

        [Required]
        [Range(1, maximum: 20, ErrorMessage = "Rooms Must be bigger than Zero and smaller than 20")]
        public int NumberOfRooms { get; set; }

        public List<RoomForPostDto> Rooms { get; set; } = new List<RoomForPostDto>();
        public int? BranchID { get; set; }



    }
}
