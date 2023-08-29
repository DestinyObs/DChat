using DChat.Models;
using Microsoft.AspNetCore.SignalR;

namespace DChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message) =>
            await Clients.All.SendAsync("recievedMessage", message);
    }
}
