using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Slacker.Api.Hubs;

[Authorize]
public class SlackerHub : Hub
{
    public async Task SendMessageAsync(string message)
    {
        var sender = Context.ConnectionId;
        await Clients.All.SendAsync("messageReceiver", message, sender);
    }
}
