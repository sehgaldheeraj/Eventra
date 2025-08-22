using Domain.Entities;
using System;

namespace Domain.Factories
{
    public static class IssueFactory
    {
        public static Issue Create(
            string title,
            string description,
            Guid assignerId,         // The creator of the issue (mandatory)
            User assigner,
            Guid? parentIssueId = null,
            Guid? sprintId = null,
            Sprint? sprint = null,
            Guid? assigneeId = null,
            User? assignee = null
        )
        {
            var issue = new Issue(title, description);

            // 🔹 Always set assigner — every issue must be created by someone
            issue.SetAssigner(assignerId, assigner);

            // 🔹 Optional: assign to a parent issue
            if (parentIssueId.HasValue)
            {
                issue.AssignParent(parentIssueId.Value);
            }

            // 🔹 Optional: assign to sprint
            if (sprintId.HasValue && sprint != null)
            {
                issue.AssignToSprint(sprintId.Value, sprint);
            }

            // 🔹 Optional: assign an initial assignee (can be left unassigned)
            if (assigneeId.HasValue && assignee != null)
            {
                issue.AssignAssignee(assigneeId.Value, assignee);
            }

            // Status auto-updates inside Issue entity
            return issue;
        }
    }
}
