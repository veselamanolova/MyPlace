
namespace MyPlace.Services
{
    using System.Linq;
    using MyPlace.Data;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using MyPlace.Services.Contracts;
    using MyPlace.DataModels;
    using AutoMapper;

    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AdminService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateEntityAsync(string title, string address, string description, string ImageUrl)
        {
            await _context.Entities.AddAsync(new Entity(title, address, description, ImageUrl));
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int entityId, int commentId)
        {
            var entity = _context.Entities.Where(e => e.Id.Equals(entityId))
                .Include(c => c.Comments).FirstOrDefault();

            var comment = entity.Comments.Where(c => c.Id.Equals(commentId)).FirstOrDefault();

            entity.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }


        public async Task EditCommentAsync(int entityId, int commentId, string newComment)
        {
            var entity = _context.Entities.Where(e => e.Id == entityId)
                .Include(c => c.Comments).FirstOrDefault();

            entity.Comments.Where(c => c.Id == commentId).Select(x => x.Content = newComment);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<string>> RegisteredUsers() =>
            await _context.Users.Select(name => name.UserName).ToListAsync();


        public async Task<IEnumerable<TSource>> GetActivity<TSource>() =>
            await Task.Run(() => _mapper.ProjectTo<TSource>(_context.EventLogs).ToListAsync());
    }
}


