using BullsAndCows;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using WebProject.GameClasses;

namespace WebProject.Hubs
{
    public class GameHub : Hub<IGameHub>
    {

        private GameMiddleware Middleware { get; }
        private GameController Controller { get; }




        public GameHub(GameMiddleware middleware, GameController controller)
        {
            Middleware = middleware;
            Controller = controller;



        }

        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            if (!Middleware.GameStarted)
            {
                var _ = Task.Run(Controller.Start);
                Middleware.GameStarted = true;
            }
        }


        public Task UserInput(string input)
        {
            Middleware.UserInput = input;
            return Task.CompletedTask;
        }

        public Task PlayAgain(bool continueGame)
        {
            Middleware.Continue = continueGame;
            if (!continueGame)
                Middleware.GameStarted = false;

            return Task.CompletedTask;
        }
    }
}
