using Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class RegisterUserDto
    {
        [Required]
        [StringLength(14, MinimumLength = 14)]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "NationalID must be exactly 14 digits.")]
        public string NationalID { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "PhoneNumber must be exactly 11 digits.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "PhoneNumber must be exactly 11 digits.")]
        public string PhoneNumber { get; set; }

        public string Name { get; set; }
        public string UserName { get; set; }

        public AgeCategory AgeCategory { get; set; }

        [Required, StringLength(128)]
        public string Email { get; set; }

        [Required, StringLength(256)]
        public string Password { get; set; }

        [Required, StringLength(256)]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string RepeatPassword { get; set; }
    }
}
