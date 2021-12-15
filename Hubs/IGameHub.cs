using GameEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebProject.Hubs
{
    public interface IGameHub
    {
        Task ReceiveMessage(string message);
        Task RecieveQuestion(string question);
        Task RecieveHiscores(IEnumerable<Player> hiscores);
        Task ClearScreen();
    }
}