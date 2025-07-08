using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Contracts.Services;
using TaskManagement.Core.Domain.Repositories;
using TaskManagement.Core.Domain.Tasks;

namespace TaskManagement.Core.ApplicationServices
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repo;

        public TaskService(ITaskRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Taska>> GetAllAsync() =>
            await _repo.GetAllAsync();

        public async Task<Taska?> GetByIdAsync(int id) =>
            await _repo.GetByIdAsync(id);

        public async Task<Taska> CreateAsync(Taska t)
        {
            t.CreatedDate = DateTime.Now;
            await _repo.AddAsync(t);
            return t;
        }

        public async Task<bool> UpdateAsync(int id, Taska updatedTask)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Title = updatedTask.Title;
            existing.Status = updatedTask.Status;

            await _repo.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
