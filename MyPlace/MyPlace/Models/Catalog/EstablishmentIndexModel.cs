
namespace MyPlace.Models.Catalog
{
    using System.Collections.Generic;
    using MyPlace.DataModels;

    public class EstablishmentIndexModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string NewPost { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}

