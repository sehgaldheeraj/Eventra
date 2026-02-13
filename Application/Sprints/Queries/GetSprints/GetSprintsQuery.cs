using Domain.Entities.Sprints;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sprints.Queries.GetSprints
{
    public class GetSprintsQuery : IRequest<IEnumerable<Sprint>>
    {
        public Guid ProjectId { get; set; }   // required
        public string? Title { get; set; }
        public SprintStatus? Status { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
