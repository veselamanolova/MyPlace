
namespace MyPlace.Data.Models
{
    using System;

    public class Note
    {
        public int Id { get; set; }

        public int EntityId { get; set; }

        public string Text { get; set; }

        public int CategoryId { get; set; }

        public DateTime Date { get; set; }

        public bool IsCompleted { get; set; }

        public Entity Entity { get; set; }

        public Category Category { get; set; }
    }
}
