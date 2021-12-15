using GameEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebProject.Hubs
{
    public interface INotificationHub
    {
        Task SendMessage(string message);
        Task ReceiveMessage(string message);
        Task UserInput(string input);
        Task SendQuestion(string v);
        Task PlayAgain(bool cont);
        Task SendHiscores(IEnumerable<Player> hiscores);
        Task ClearScreen();
    }
}