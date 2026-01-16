using Domain.Entities;
using Domain.Entities.Projects;
using Domain.Interfaces;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class NoticeRepository(EventraDBContext context) : INoticeRepository
    {
        private readonly EventraDBContext _context = context;
        public async Task CreateNoticeAsync(Notice notice, CancellationToken ct)
        {
            _context.Notices.Add(notice);
            await _context.SaveChangesAsync(ct);  
        }
    }
}
