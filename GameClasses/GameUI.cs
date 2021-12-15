using GameEngine;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using WebProject.Hubs;

namespace WebProject.GameClasses
{
    public class GameUI : IGameUI
    {

        public GameUI(IHubContext<GameHub, IGameHub> hubContext, GameMiddleware middleware)
        {
            HubContext = hubContext;
            Middleware = middleware;
        }

        public IHubContext<GameHub, IGameHub> HubContext { get; }
        public GameMiddleware Middleware { get; }

        public void Clear()
        {
            HubContext.Clients.All.ClearScreen();
        }

        public bool Continue()
        {
            HubContext.Clients.All.RecieveQuestion("Want to continue?");
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
            HubContext.Clients.All.RecieveHiscores(hiscores);
        }
    }
}
