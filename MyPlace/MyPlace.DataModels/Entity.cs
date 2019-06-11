
namespace MyPlace.DataModels
{
    using System.Collections.Generic;

    public class Entity
    {
        public Entity() { }

        public Entity(string title, string address, string description, string imageUrl)
        {
            Title = title;
            Address = address;
            Description = description;
            ImageUrl = imageUrl;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int? EstablishmentId { get; set; }

        public Entity Establishment { get; set; }

        public ICollection<Entity> LogBooks { get; set; }

        public ICollection<EntityCategory> EntityCategories { get; set; }

        public ICollection<UserEntity> UserEntities { get; set; }

        public ICollection<Note> Notes { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
