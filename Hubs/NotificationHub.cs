using BullsAndCows;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using WebProject.Controllers;

namespace WebProject.Hubs
{
    public class NotificationHub : Hub<INotificationHub>
    {
        public NotificationHub(GameMiddleware middleware, GameController controller)
        {
            Middleware = middleware;
            Controller = controller;

            
        }
        private bool GameStarted { get; set; }
        public GameMiddleware Middleware { get; }
        public GameController Controller { get; }

        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            if (!GameStarted)
            {
                var _ = Task.Run(Controller.Start);
                GameStarted = true;
            }
        }


        public Task UserInput(Message input)
        {
            Middleware.UserInput = input.Text;
            return Task.CompletedTask;
        }

        public Task PlayAgain(bool cont)
        {
            Middleware.Continue = cont;
            if (!cont)
                GameStarted = false;

            return Task.CompletedTask;
        }
    }
}
