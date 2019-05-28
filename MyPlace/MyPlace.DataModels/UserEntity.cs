
namespace MyPlace.DataModels
{
    public class UserEntity
    {
        public string UserId { get; set; }

        public int EntityId { get; set; }

        public User User { get; set; }

        public Entity Entity { get; set; }
    }
}
