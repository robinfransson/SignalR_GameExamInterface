using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WebProject.GameClasses;
using WebProject.Hubs;

namespace WebProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        public NotificationController(IHubContext<GameHub, IGameHub> hubContext, GameMiddleware middleware)
        {
            HubContext = hubContext;
            Middleware = middleware;
        }

        public IHubContext<GameHub, IGameHub> HubContext { get; }
        public GameMiddleware Middleware { get; }

        [HttpPost]
        public async Task<IActionResult> NotificationOnPostAsync([FromBody] string notification)
        {
            await HubContext.Clients.All.ReceiveMessage(notification);
            return Ok();
        }

        
    }
}
