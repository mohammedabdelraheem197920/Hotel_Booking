using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DTOs
{
    public class BookingForGetDto
    {
        public int ID { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount { get; set; }
        public bool DiscountApplied { get; set; }
        public int BranchID { get; set; }
        public int UserId { get; set; }

    }
}
