
namespace MyPlace.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class ApplicationUser : IdentityUser
    {
        public List<UserEntity> UserEntities { get; set; }
    }
}
