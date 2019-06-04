
namespace MyPlace.Services.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface ICatalogService
    {
        Task<IQueryable<TSource>> ReadAllAsync<TSource>();

        Task<TSource> GetByIdAsync<TSource>(int Id);

        Task CreateReplyAsync(int Id, string user, string text);

        Task<IEnumerable<string>> AutocompleteGetAll();

        Task<IEnumerable<string>> GetForbiddenWords();
    }
}

