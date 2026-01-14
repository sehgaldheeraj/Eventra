using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Broadcasters
{
    public interface INoticeBroadcaster
    {
        Task BroadcastProjectCreatedAsync(
            Guid projectId,
            string projectName,
            Guid ownerId,
            CancellationToken ct = default
        );
    }
}
