using Application.Common.Interfaces.Broadcasters;
using Eventra.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Eventra.Broadcasting
{
    public class SignalRNoticeBroadcaster(IHubContext<EventraHub> hub) : INoticeBroadcaster
    {
        private readonly IHubContext<EventraHub> _hub = hub;
        public async Task BroadcastProjectCreatedAsync(
            Guid projectId,
            string projectName,
            Guid ownerId,
            CancellationToken ct = default
        )
        {
            await _hub.Clients.User(ownerId.ToString())
                .SendAsync(
                    "ProjectCreated", 
                    new 
                    {
                        ProjectId = projectId,
                        Name = projectName
                    }, 
                    ct);
        }
    }
}
