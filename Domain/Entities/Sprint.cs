using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Sprint
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public string? Goal { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public SprintStatus Status { get; set; } = SprintStatus.Planned;

        [ForeignKey(nameof(ProjectId))]
        public Project? Project { get; set; }

        // Future: public ICollection<Issue> Issues { get; set; }

    }
    public enum SprintStatus
    {
        Planned = 0,
        Active = 1,
        Completed = 2,
        Cancelled = 3
    }
}
