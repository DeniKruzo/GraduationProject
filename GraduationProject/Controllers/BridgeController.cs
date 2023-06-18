using GraduationProject.Data;
using GraduationProject.Hubs;
using GraduationProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NuGet.Packaging.Signing;

namespace GraduationProject.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class BridgeController : Controller
    {
        private IHubContext<ChatHub> _chat;

        public BridgeController(
            IHubContext<ChatHub> chat)
        {
            _chat = chat;
        }

        [HttpPost("[action]/{connectionId}/{roomId}")]
        public async Task<IActionResult> JoinRoom(string connectionId
                                                    , string roomId)
        {
            await _chat.Groups.AddToGroupAsync(connectionId, roomId);
            return Ok();
        }

        [HttpPost("[action]/{connectionId}/{roomId}")]
        public async Task<IActionResult> LeaveRoom(string connectionId
                                               , string roomId)
        {
            await _chat.Groups.RemoveFromGroupAsync(connectionId, roomId);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendMessage(long roomId, string message,
                                                     [FromServices] GraduationDbContext context)
        {
            var twit = new Message
            {
                ChatId = roomId,
                Text = message,
                Name = User.Identity.Name,
                TimeStamp = DateTime.Now
            };

            context.Messages.Add(twit);

            await context.SaveChangesAsync();

            await _chat.Clients.Group(roomId.ToString())
                .SendAsync("ReceiveMessage", new
                {
                    Text = twit.Text,
                    Name = twit.Name,
                    Timestamp = twit.TimeStamp.ToString("hh:mm")
                }); 

            return Ok();
        }
    }
}
