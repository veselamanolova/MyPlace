
namespace MyPlace.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using MyPlace.Data;
    using MyPlace.Data.Models;
    using MyPlace.Services.Contracts;
    using AutoMapper;

    public class CatalogService : ICatalogService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

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

        public IEnumerable<string> AutocompleteGetAll() => 
            _context.Entities.Select(entity => entity.Title);
    }
}

