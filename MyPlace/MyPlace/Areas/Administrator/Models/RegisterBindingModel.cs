
using System.ComponentModel.DataAnnotations;

namespace MyPlace.Models.Account
{
    public class RegisterBindingModel
    {
        [Required] 
        [StringLength(30, MinimumLength = 4, ErrorMessage = "User name must be between 3 and 30 characters. ")]
        public string UserName { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters. ")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Role { get; set; }
    }
}

