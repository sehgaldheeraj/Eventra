using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Notification
    {
        public Guid Id { get; private set; }

        // Target
        public Guid UserId { get; private set; }

        // Source context (same philosophy as Notice)
        public NoticeContext ContextType { get; private set; }
        public Guid? ContextId { get; private set; }

        // Semantics (reused)
        public NoticeKind Kind { get; private set; }
        public NoticeSeverity Severity { get; private set; }

        // Payload
        public string Title { get; private set; }
        public string Message { get; private set; }

        // Lifecycle
        public bool IsRead { get; private set; }
        public DateTime? ReadAt { get; private set; }

        public bool IsAcknowledged { get; private set; }
        public DateTime? AcknowledgedAt { get; private set; }

        // Navigation helper (deep-linking)
        public string? ActionUrl { get; private set; }

        // System
        public DateTime CreatedAt { get; private set; }

        private Notification() { } // EF
    }
}
