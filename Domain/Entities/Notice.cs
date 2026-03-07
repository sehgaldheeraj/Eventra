using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Notice
    {
        public Guid Id { get; private set; }

        public Guid SenderId { get; private set; }     // System or User

        public NoticeContext ContextType { get; private set; }
        public Guid? ContextId { get; private set; }   // IssueId / ProjectId / UserId

        public NoticeKind Kind { get; private set; }
        public NoticeSeverity Severity { get; private set; }

        public NoticeType Type { get; private set;}

        public string Message { get; private set; }

        public Guid? ReplyToNoticeId { get; private set; } // flat replies

        public DateTime CreatedAt { get; private set; }
        private Notice() { } // EF

        private Notice(
            Guid senderId,
            NoticeContext contextType,
            Guid? contextId,
            NoticeKind kind,
            NoticeSeverity severity,
            NoticeType type,
            string message,
            Guid? replyToNoticeId)
        {
            Id = Guid.NewGuid();
            SenderId = senderId;
            ContextType = contextType;
            ContextId = contextId;
            Kind = kind;
            Severity = severity;
            Type = type;
            Message = message;
            ReplyToNoticeId = replyToNoticeId;
            CreatedAt = DateTime.UtcNow;
        }

        public static Notice Create(
           Guid senderId,
           NoticeContext contextType,
           Guid? contextId,
           NoticeKind kind,
           NoticeSeverity severity,
           NoticeType type,
           string message,
           Guid? replyToNoticeId = null)
        {
            return new Notice(
                senderId,
                contextType,
                contextId,
                kind,
                severity,
                type,
                message,
                replyToNoticeId);
        }
    }
    public enum NoticeContext
    {
        Issue,
        Project,
        Sprint,
        User,        // DM
        Global       // Rare, system-wide
    }
    public enum NoticeKind
    {
        Message,        // Human-written text
        Assignment,     // Ownership changes
        StatusChange,   // Open → InProgress → Done
        Progress,       // SLA / lag / ETA
        Decision,       // Accept / Reject / Approve
        SystemEvent,    // CI/CD, automation
        StructuralLifecycle
    }
    public enum NoticeSeverity
    {
        Info,       // FYI
        Success,    // Positive system confirmation
        Warning,    // Needs attention
        Critical    // Immediate action required
    }
    public enum NoticeType
    {
        ProjectCreated,
        ProjectArchived,
        ProjectRestored,
        ProjectOwnerChanged,

        SprintStarted,
        SprintCompleted,

        IssueCreated,
        IssueAssigned,
        IssueStatusChanged,
        IssueMovedToInProgress,
        IssueClosed,
        IssueReopened,
        IssueAddedToSprint,
        IssueRemovedFromSprint,
        IssueUnassigned,

        System // fallback
    }

}
