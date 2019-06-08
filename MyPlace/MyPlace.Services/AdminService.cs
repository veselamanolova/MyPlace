
namespace MyPlace.Services
{
    using System.Linq;
    using MyPlace.Data;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using MyPlace.Services.Contracts;
    using MyPlace.DataModels;

    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;

        public AdminService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateEntityAsync(string title, string address, string description, string ImageUrl)
        {
            await _context.Entities.AddAsync(new Entity(title, address, description, ImageUrl));
            await _context.SaveChangesAsync();
        }


        public void Delete(int entityId, int commentId)
        {
            var entity = _context.Entities.Where(e => e.Id.Equals(entityId))
                .Include(c => c.Comments).FirstOrDefault();

            var comment = entity.Comments.Where(c => c.Id.Equals(commentId)).FirstOrDefault();

            entity.Comments.Remove(comment);
            _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<string>> RegisteredUsers() =>
            await _context.Users.Select(name => name.UserName).ToListAsync();
    }
}
