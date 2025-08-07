using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Infrastructure.Persistence
{
    public class EventraDBContext : DbContext
    {
        public EventraDBContext(DbContextOptions<EventraDBContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Sprint> Sprints { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(u => u.Id);

                entity.Property(u => u.Email)
                      .IsRequired();

                entity.HasIndex(u => u.Email)
                      .IsUnique();

                entity.Property(u => u.Name)
                      .IsRequired();

                entity.Property(u => u.Password)
                      .IsRequired();
            });

            // Project
            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Projects");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                      .IsRequired();

                entity.HasIndex(p => p.Name)
                      .IsUnique(); // Optional
            });

            // Sprint
            modelBuilder.Entity<Sprint>(entity =>
            {
                entity.ToTable("Sprints");

                entity.HasKey(s => s.Id);

                entity.Property(s => s.Title)
                      .IsRequired();

                entity.HasIndex(s => s.Title)
                      .IsUnique(); // Enforce unique sprint titles

                entity.Property(s => s.Status)
                      .HasConversion<int>(); // Enum as int
            });
        }
    }
}
