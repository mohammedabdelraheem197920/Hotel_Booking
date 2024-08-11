using Core.RepositoryInterfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Booking : IEntity
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfRooms { get; set; }
        public bool DiscountApplied { get; set; }
        public DateTime BookingDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discount { get; set; }



        public List<Room> Rooms { get; set; } = new List<Room>();


        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        public Branch Branch { get; set; }


        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
