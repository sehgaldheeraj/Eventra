namespace Domain.Entities
{
    public class Notification
    {
        public Guid Id { get; private set; }

        // Target (who receives it)
        public Guid UserId { get; private set; }

        // Source Notice (single source of truth)
        public Guid NoticeId { get; private set; }

        // Lifecycle
        public bool IsRead { get; private set; }
        public DateTime? ReadAt { get; private set; }

        public bool IsAcknowledged { get; private set; }
        public DateTime? AcknowledgedAt { get; private set; }

        // Optional deep link helper
        public string? ActionUrl { get; private set; }

        public DateTime CreatedAt { get; private set; }

        private Notification() { } // EF

        private Notification(
            Guid userId,
            Guid noticeId,
            string? actionUrl)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            NoticeId = noticeId;
            ActionUrl = actionUrl;
            CreatedAt = DateTime.UtcNow;
        }

        public static Notification Create(
            Guid userId,
            Guid noticeId,
            string? actionUrl = null)
        {
            return new Notification(userId, noticeId, actionUrl);
        }

        public void MarkAsRead()
        {
            if (IsRead) return;

            IsRead = true;
            ReadAt = DateTime.UtcNow;
        }

        public void Acknowledge()
        {
            if (IsAcknowledged) return;

            IsAcknowledged = true;
            AcknowledgedAt = DateTime.UtcNow;
        }
    }
}
