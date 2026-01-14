using Domain.Common;
using Domain.Entities.Projects.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Projects
{
    public class Project : Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } 
        public Guid OwnerId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public User Owner { get; set; }
        public ICollection<Sprint> Sprints { get; private set; } = [];
        public ICollection<Issue> Issues { get; private set; } = [];

        public Project() { } // EF Core ctor
        public Project(string name, Guid ownerId, string? description = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Project name is required.", nameof(name));
            Id = Guid.NewGuid();
            Name = name;
            OwnerId = ownerId;
            Description = description;
            CreatedAt = DateTime.UtcNow;

            AddDomainEvent(new ProjectCreated(Id, Name, OwnerId));
        }
        public void UpdateDetails(
           string? name = null,
           string? description = null,
           Guid? ownerId = null)
        {
            if (!string.IsNullOrWhiteSpace(name))
                Name = name;
            if (description != null)
                Description = description;
            if (ownerId.HasValue)
                OwnerId = ownerId.Value;
        }   
    }
}
