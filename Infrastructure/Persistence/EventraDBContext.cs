using Application.Common.Interfaces.Dispatchers;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.Issues;
using Domain.Entities.Projects;
using Domain.Entities.Sprints;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class EventraDBContext
        : DbContext
    {
        private readonly IDomainEventDispatcher _dispatcher;

        public EventraDBContext(
            DbContextOptions<EventraDBContext> options,
            IDomainEventDispatcher dispatcher)
            : base(options)
        {
            _dispatcher = dispatcher;
        }

        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Sprint> Sprints { get; set; } = null!;
        public DbSet<Issue> Issues { get; set; } = null!;
        public DbSet<Notice> Notices { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Email).IsRequired();
                entity.HasIndex(u => u.Email).IsUnique();

                entity.Property(u => u.Name).IsRequired();
                entity.Property(u => u.Password).IsRequired();
            });

            // Project
            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Projects");
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name).IsRequired();
                entity.HasIndex(p => p.Name).IsUnique();
            });

            // 🔥 Soft delete filter (explicit, safe)
            modelBuilder.Entity<Project>()
                .HasQueryFilter(p => p.DeletedAt == null);

            // Sprint
            modelBuilder.Entity<Sprint>(entity =>
            {
                entity.ToTable("Sprints");
                entity.HasKey(s => s.Id);

                entity.Property(s => s.Title).IsRequired();
                entity.HasIndex(s => s.Title).IsUnique();

                entity.Property(s => s.Status)
                      .HasConversion<int>();
            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            var domainEvents = ChangeTracker
                .Entries<Entity>()
                .SelectMany(e => e.Entity.DomainEvents)
                .ToList();

            var result = await base.SaveChangesAsync(ct);

            await _dispatcher.DispatchAsync(domainEvents, ct);

            foreach (var entry in ChangeTracker.Entries<Entity>())
            {
                entry.Entity.ClearDomainEvents();
            }

            return result;
        }
    }
}
