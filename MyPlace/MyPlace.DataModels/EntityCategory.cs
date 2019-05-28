
namespace MyPlace.DataModels
{
    public class EntityCategory
    {
        public int EntityId { get; set; }

        public int CategoryId { get; set; }

        public Entity Entity { get; set; }

        public Category Category { get; set; }
    }
}
