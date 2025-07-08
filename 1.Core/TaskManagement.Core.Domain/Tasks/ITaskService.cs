using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Core.Domain.Tasks
{
    public interface ITaskService
    {
        Task<List<Taska>> GetAllAsync();
        Task<Taska?> GetByIdAsync(int id);
        Task<Taska> CreateAsync(Task task);
        Task<bool> UpdateAsync(int id, Taska updatedTask);
        Task<bool> DeleteAsync(int id);

    }
}
