using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Notice
    {
        public Guid Id { get; set; }

        public Guid SenderId { get; set; }     // System or User

        public NoticeContext ContextType { get; set; }
        public Guid? ContextId { get; set; }   // IssueId / ProjectId / UserId

        public NoticeKind Kind { get; set; }
        public NoticeSeverity Severity { get; set; }

        public string Message { get; set; }

        public Guid? ReplyToNoticeId { get; set; } // flat replies

        public DateTime CreatedAt { get; set; }
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
        SystemEvent     // CI/CD, automation
    }
    public enum NoticeSeverity
    {
        Info,       // FYI
        Success,    // Positive system confirmation
        Warning,    // Needs attention
        Critical    // Immediate action required
    }
}
