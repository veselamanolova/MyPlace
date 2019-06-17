
namespace MyPlace.Models.Catalog
{
    public class CatalogListingModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string ImageUrl { get; set; }

        public int? EstablishmentId { get; set; }
    }
}
