using System;
using System.Collections.Generic;
using System.Text;

namespace MyPlace.Services.DTOs
{
    public class EntityCategoryDTO
    {
        public int CategoryId { get; set; }

        public int EntityId { get; set; }

        public string Name { get; set; }
    }
}
