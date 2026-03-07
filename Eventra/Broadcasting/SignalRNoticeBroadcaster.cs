using Application.Common.Interfaces.Broadcasters;
using Domain.Entities;
using Eventra.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Eventra.Broadcasting
{
    public class SignalRNoticeBroadcaster(IHubContext<EventraHub> hub) : INoticeBroadcaster
    {
        private readonly IHubContext<EventraHub> _hub = hub;

        public async Task BroadcastAsync(Notice notice, Guid userId, CancellationToken ct = default)
        {
            await _hub.Clients.User(userId.ToString())
                .SendAsync(
                    notice.Type.ToString(), 
                    notice,
                    ct
                );
        }
    }
}
