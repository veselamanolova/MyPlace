
namespace MyPlace.Services.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IAdminService
    {
        Task DeleteAsync(int entityId, int commentId);

        Task EditCommentAsync(int entityId, int commentId, string newComment);

        Task<IEnumerable<string>> RegisteredUsers();

        Task CreateEntityAsync(string title, string address, string description, string ImageUrl);

        Task<IEnumerable<TSource>> GetActivity<TSource>();
    }
}