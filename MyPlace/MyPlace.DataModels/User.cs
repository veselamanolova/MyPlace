
namespace MyPlace.DataModels
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public ICollection<UserEntity> UserEntities { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}
