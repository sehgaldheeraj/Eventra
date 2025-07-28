using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProjectRepository
    {
        Task AddAsync(Project project);
        Task<List<Project>> GetAllAsync();
        Task<Project?> GetAsync(Guid id);
        Task UpdateAsync(Project project);
        Task DeleteAsync(Project project);
    }
}
