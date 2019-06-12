
namespace MyPlace.Hubs
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;
    using MyPlace.Services.Contracts;

    public class CommentHub : Hub
    {
        private readonly ICatalogService _catalogService;

        public CommentHub(ICatalogService catalogService) =>
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));

        public async Task SendMessage(int Id, string user, string comment)
        {
            bool isOffensive = false;
            var forbiddenWords = _catalogService.GetForbiddenWords().Result;

            foreach (var word in System.Text.RegularExpressions.Regex.Split(comment, "\\s+"))
                if (forbiddenWords.Contains(word.ToLower()))
                {
                    isOffensive = true;
                    break;
                }
             
            if (isOffensive)
            {
                await Clients.All.SendAsync("OffensiveComment", "This comment is offensive!");
                return;
            }
            await _catalogService.CreateReplyAsync(Id, user, comment);
            await Clients.All.SendAsync("AddComment", comment, Id);
        }
    }
}

