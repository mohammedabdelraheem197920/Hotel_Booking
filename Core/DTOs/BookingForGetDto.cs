using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DTOs
{
    public class BookingForGetDto
    {
        public int Id { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount { get; set; }
        public bool DiscountApplied { get; set; }
        public int numberOfRooms { get; set; }
        public int BranchId { get; set; }
        public string UserId { get; set; }

    }
}
