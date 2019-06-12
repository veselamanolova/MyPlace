namespace MyPlace.Services.DTOs
{
    using System.Collections.Generic;

    public class CompositeEntityCategoriesDTO
    {
        public List<CategoryDTO> EntityCategories { get; set; }
        public List<CategoryDTO> AllNotEntityCategories { get; set; }
    }
}
