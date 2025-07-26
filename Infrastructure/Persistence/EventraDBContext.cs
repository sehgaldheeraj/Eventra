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
    }
}
