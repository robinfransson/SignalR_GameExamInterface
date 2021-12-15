using GameEngine;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WebProject.Hubs;

namespace WebProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        public NotificationController(IHubContext<NotificationHub, INotificationHub> hubContext, GameMiddleware middleware)
        {
            HubContext = hubContext;
            Middleware = middleware;
        }

        public IHubContext<NotificationHub, INotificationHub> HubContext { get; }
        public GameMiddleware Middleware { get; }

        [HttpPost]
        public async Task<IActionResult> NotificationOnPostAsync([FromBody] string notification)
        {
            var message = new Message
            {
                Text = notification,
                User = "admin"
            };
            await HubContext.Clients.All.ReceiveMessage(message);
            return Ok();
        }

        [HttpGet]

        public async Task<IActionResult> GetCurrentGameState()
        {
            if (Middleware.Output != null)
            {
                var message = new Message
                {
                    Text = Middleware.Output
                };
                await HubContext.Clients.All.ReceiveMessage(message);

                return Ok();

            }

            return Ok();

        }
        
    }
}
