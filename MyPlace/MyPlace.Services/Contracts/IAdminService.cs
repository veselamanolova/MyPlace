
namespace MyPlace.Services.Contracts
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IAdminService
    {
        Task Delete(int entityId, int commentId);

        Task<IEnumerable<string>> RegisteredUsers();

        Task CreateEntityAsync(string title, string address, string description, string ImageUrl);
    }
}