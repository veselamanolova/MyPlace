
namespace MyPlace.DataModels
{
    using System.Collections.Generic;

    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<EntityCategory> EntityCategories { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}
