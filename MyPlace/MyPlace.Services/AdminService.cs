
namespace MyPlace.Services
{
    using System.Linq;
    using MyPlace.Data;
    using MyPlace.Services.Contracts;
    using Microsoft.EntityFrameworkCore;

    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;

        public AdminService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Delete(int entityId, int commentId)
        {
            var entity = _context.Entities.Where(e => e.Id.Equals(entityId))
                .Include(c => c.Comments).FirstOrDefault();

            var comment = entity.Comments.Where(c => c.Id.Equals(commentId)).FirstOrDefault();

            entity.Comments.Remove(comment);
            _context.SaveChanges();
        }
    }
}
