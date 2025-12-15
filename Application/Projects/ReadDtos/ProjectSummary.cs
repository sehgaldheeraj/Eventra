using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Projects.ReadDtos
{
    public class ProjectSummary
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public string OwnerRole { get; set; } = string.Empty;
    }
}
