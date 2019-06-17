
namespace MyPlace.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MyPlace.Services.DTOs;

    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllCategoriesAsync();

        Task<List<CategoryDTO>> GetAllLogBooksCategoriesAsync(int id);

        Task<CompositeEntityCategoriesDTO> GetAllLogBookAndNotLogBookCategories(int id);

        Task<CategoryDTO> FindCategoryByIdAsync(int id);

        Task AddCategoryAsync(string name);

        Task EditCategoryAsync(int id, string name);

        Task DeleteCategoryAsync(int id); 
    }
}


