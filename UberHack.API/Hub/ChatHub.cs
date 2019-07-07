using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace UberHack.API.Hub
{
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public Task SendMessage(ChatMessage message)
        {
            return Clients.Group(message.ChatId).SendAsync("ReceiveMessage", message.Content);
        }

        public Task Join(string groupId)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }
    }
}