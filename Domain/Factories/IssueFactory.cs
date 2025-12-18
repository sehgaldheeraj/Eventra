using Domain.Entities;
using System;

namespace Domain.Factories
{
    public static class IssueFactory
    {
        public static Issue Create(
            string title,
            string description,
            Guid projectId,
            Guid assignerId,
            Guid? parentIssueId = null,
            Guid? sprintId = null,
            Guid? assigneeId = null
        )
        {
            // Core construction (IDs only)
            var issue = new Issue(
                title,
                description,
                projectId,
                assignerId
            );

            // Optional: parent issue
            if (parentIssueId.HasValue)
            {
                issue.SetAsSubIssue(parentIssueId.Value);
            }

            // Optional: sprint
            if (sprintId.HasValue)
            {
                issue.AssignToSprint(sprintId.Value);

                // Explicit business rule:
                // If created inside a sprint → starts in ToDo
                issue.MoveToToDo();
            }

            // Optional: assignee
            if (assigneeId.HasValue)
            {
                issue.AssignAssignee(assigneeId.Value);
            }

            return issue;
        }
    }
}
