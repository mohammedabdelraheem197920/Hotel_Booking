using Core.Enums;
using Core.RepositoryInterfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Room : IEntity
    {
        public int Id { get; set; }
        public RoomType Type { get; set; } = RoomType.Single;
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public bool IsBooked { get; set; }



        [ForeignKey("Booking")]
        public int? BookingId { get; set; }
        public Booking? Booking { get; set; }


        [ForeignKey("Branch")]
        public int? BranchID { get; set; }
        public Branch Branch { get; set; }

    }
}
