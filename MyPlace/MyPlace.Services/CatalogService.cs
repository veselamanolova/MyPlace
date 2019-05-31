
namespace MyPlace.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using MyPlace.Data;
    using MyPlace.DataModels;
    using MyPlace.Services.Contracts;
    using AutoMapper;

    public class CatalogService : ICatalogService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public CatalogService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<IQueryable<TSource>> ReadAllAsync<TSource>() =>
            await Task.Run(() => _mapper.ProjectTo<TSource>(_context.Entities));


        public async Task<TSource> GetByIdAsync<TSource>(int Id) =>
            await Task.Run(() => _mapper.ProjectTo<TSource>(
                _context.Entities
                .Where(entity => entity.Id.Equals(Id))
                    .Include(comment => comment.Comments))
                .FirstOrDefault());


        public Task CreateReplyAsync(int Id, string text)
        {
            var selectedEntity = _context.Entities
            .Where(entity => entity.Id.Equals(Id))
                .Include(comments => comments.Comments)
            .FirstOrDefault();

            selectedEntity.Comments.Add(new Comment(Id, text, DateTime.Now, selectedEntity));
            return _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> AutocompleteGetAll() => 
            await _context.Entities.Select(entity => entity.Title).ToListAsync();
    }
}

