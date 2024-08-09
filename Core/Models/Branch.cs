using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Branch
    {
        public int ID { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }


        List<Room> Rooms { get; set; } = new List<Room>();

        List<Booking> Bookings { get; set; } = new List<Booking>();


        [ForeignKey("Hotel")]
        public int HotelID { get; set; }
        public Hotel Hotel { get; set; }
    }
}
