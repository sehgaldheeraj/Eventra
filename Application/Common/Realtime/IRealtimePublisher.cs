using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Realtime
{
    public interface IRealtimePublisher
    {
        Task PublishAsync(
            Guid userId,
            string eventName,
            object payload,
            CancellationToken ct
        );
    }
}
