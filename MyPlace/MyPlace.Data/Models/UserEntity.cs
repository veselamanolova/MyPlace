
namespace MyPlace.Data.Models
{
    public class UserEntity
    {
        public string UserId { get; set; }

        public int EntityId { get; set; }

        public ApplicationUser User { get; set; }

        public Entity Entity { get; set; }
    }
}
