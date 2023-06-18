using Microsoft.AspNetCore.SignalR;

namespace GraduationProject.Hubs
{
    public class ChatHub : Hub
    {
        public string GetConnectionId() =>
            Context.ConnectionId;
    }

}
