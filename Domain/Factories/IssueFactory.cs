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
            Guid projectId,
            Project project,
            Guid? parentIssueId = null,
            Issue? parentIssue = null,
            Guid? sprintId = null,
            Sprint? sprint = null,
            Guid? assigneeId = null,
            User? assignee = null
        )
        {
            // 🔹 Constructor enforces assigner and project 
            var issue = new Issue(title, description, assignerId, assigner, projectId, project);

            if (parentIssueId.HasValue)
            {
                issue.SetAsSubIssue(parentIssueId.Value, parentIssue);
            }

            // 🔹 Optional: assign to sprint
            if (sprintId.HasValue && sprint != null)
            {
                issue.AssignToSprint(sprintId.Value, sprint);
            }

            // 🔹 Optional: assign an initial assignee
            if (assigneeId.HasValue && assignee != null)
            {
                issue.AssignAssignee(assigneeId.Value, assignee);
            }

            // 🟢 Status is explicit now. If sprint given → ToDo, else Backlog.
            if (sprintId.HasValue)
            {
                issue.MoveToToDo();
            }

            return issue;
        }
    }
}
