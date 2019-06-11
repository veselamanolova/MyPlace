using MyPlace.DataModels;
using System.Collections.Generic;

namespace MyPlace.Services.DTOs
{
    public class EntityDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int? EstablishmentId { get; set; }

        public Entity Establishment { get; set; }

        public IEnumerable<EntityDTO> LogBooks { get; set; }

        public IEnumerable<CategoryDTO> Categories { get; set; }
    }
}