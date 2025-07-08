using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Domain.Tasks;

namespace TaskManagement.Core.Contracts.Services
{
    public interface ITaskService
    {
        Task<List<Taska>> GetAllAsync();
        Task<Taska?> GetByIdAsync(int id);
        Task<Taska> CreateAsync(Taska task);
        Task<bool> UpdateAsync(int id, Taska updatedTask);
        Task<bool> DeleteAsync(int id);

    }
}
