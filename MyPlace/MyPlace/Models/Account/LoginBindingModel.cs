
using System.ComponentModel.DataAnnotations;

namespace MyPlace.Models.Account
{
    public class LoginBindingModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

