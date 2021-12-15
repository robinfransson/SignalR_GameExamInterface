using GameEngine;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using WebProject.Hubs;

namespace WebProject.Controllers
{
    public class GameUI : IGameUI
    {

        public GameUI(IHubContext<NotificationHub, INotificationHub> hubContext, GameMiddleware middleware)
        {
            HubContext = hubContext;
            Middleware = middleware;
        }

        public IHubContext<NotificationHub, INotificationHub> HubContext { get; }
        public GameMiddleware Middleware { get; }

        public void Clear()
        {
            HubContext.Clients.All.ClearScreen();
        }

        public bool Continue()
        {
            HubContext.Clients.All.SendQuestion("Want to continue?");
            return Middleware.AwaitAnswer().GetAwaiter().GetResult();
        }

        public string GetInput()
        {
            return Middleware.AwaitInput().GetAwaiter().GetResult();
            
        }

        public void Output(string s)
        {
            HubContext.Clients.All.ReceiveMessage(s);
        }

        public void ShowHiscores(IEnumerable<Player> hiscores)
        {
            HubContext.Clients.All.SendHiscores(hiscores);
        }
    }
}
