using Domain.Common;
using Domain.Entities.Issues;
using Domain.Entities.Projects.Events;
using Domain.Entities.Sprints;

namespace Domain.Entities.Projects
{
    public class Project : SoftDeletableEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public Guid OwnerId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public User Owner { get; private set; }

        public ICollection<Sprint> Sprints { get; private set; } = [];
        public ICollection<Issue> Issues { get; private set; } = [];

        private Project() { } // EF Core only

        private Project(string name, Guid ownerId, string? description)
        {
            Id = Guid.NewGuid();
            Name = name;
            OwnerId = ownerId;
            Description = description;
            CreatedAt = DateTime.UtcNow;
        }

        public static Project Create(
            string name,
            Guid ownerId,
            string? description = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Project name is required.", nameof(name));

            var project = new Project(name, ownerId, description);

            project.AddDomainEvent(
                new ProjectCreated(project.Id, project.Name, project.OwnerId)
            );

            return project;
        }
        public void UpdateDetails(string? name = null, string? description = null)
        {
            if (IsDeleted)
                throw new InvalidOperationException("Cannot update a deleted project.");

            if (!string.IsNullOrWhiteSpace(name))
                Name = name;

            if (description != null)
                Description = description;
        }

        public void ChangeOwner(Guid newOwnerId)
        {
            if (IsDeleted)
                throw new InvalidOperationException("Cannot change owner of a deleted project.");

            if (OwnerId == newOwnerId)
                return;

            var oldOwnerId = OwnerId;
            OwnerId = newOwnerId;

            AddDomainEvent(
                new ProjectOwnerChanged(Id, Name, oldOwnerId, newOwnerId)
            );
        }

        public void Delete(Guid deletedByUserId)
        {
            if (IsDeleted)
                return;

            MarkDeleted();

            AddDomainEvent(
                new ProjectArchived(
                    Id,
                    Name,
                    deletedByUserId
                )
            );
        }
    }
}
