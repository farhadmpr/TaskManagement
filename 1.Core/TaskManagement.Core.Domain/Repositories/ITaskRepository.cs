using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Domain.Tasks;

namespace TaskManagement.Core.Domain.Repositories
{
    public interface ITaskRepository
    {
        Task<Taska?> GetByIdAsync(int id);
        Task<List<Taska>> GetAllAsync();
        Task AddAsync(Taska task);
        Task UpdateAsync(Taska task);
        Task DeleteAsync(int id);

    }
}
