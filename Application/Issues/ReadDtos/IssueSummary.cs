using Domain.Entities.Issues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Issues.ReadDtos
{
    public class IssueSummary
    {
        public Guid IssueId { get; set; }
        public string Title { get; set; }
        public string? AssigneeName { get; set; }
        public IssueStatus Status { get; set; }
        public int SubIssueCount { get; set; }
        public int NoticeCount { get; set; } // optional, messages/updates
    }
}
