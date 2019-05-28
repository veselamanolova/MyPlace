
namespace MyPlace.DataModels
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public List<UserEntity> UserEntities { get; set; }
    }
}
