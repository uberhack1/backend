using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace UberHack.API.Hub
{
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}