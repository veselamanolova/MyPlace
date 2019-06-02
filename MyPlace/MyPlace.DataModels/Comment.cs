
namespace MyPlace.DataModels
{
    using System;

    public class Comment
    {
        public Comment() { }

        public Comment(int entityId, string author, string content, DateTime created, Entity entity)
        {
            EntityId = entityId;
            Author = author;
            Content = content;
            CreatedOn = created;
            Entity = entity;
        }

        public int Id { get; set; }

        public int EntityId { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Rating { get; set; }

        public Entity Entity { get; set; }
    }
}
