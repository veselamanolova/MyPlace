
namespace MyPlace.Hubs
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;
    using MyPlace.Services.Contracts;

    public class CommentHub : Hub
    {
        private readonly ICatalogService _catalogService;

        public CommentHub(ICatalogService catalogService) =>
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));

        public async Task SendMessage(int Id, string comment)
        {
            await _catalogService.CreateReplyAsync(Id, comment);
            await Clients.All.SendAsync("AddComment", comment);
        }
    }
}
