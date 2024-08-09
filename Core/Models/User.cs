using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class User : IdentityUser
    {
        [Required]
        [RegularExpression(@"^\d{14}$")]
        [Length(14, 14, ErrorMessage = "NationalID must be  14 digits.")]
        public string NationalID { get; set; }
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^\d{11}$")]
        [Length(11, 11, ErrorMessage = "PhoneNumber must be 11 digits.")]
        public string PhoneNumber { get; set; }
        public bool isOldClient { get; set; } = false;

        List<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
