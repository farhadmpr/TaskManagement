using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Core.Domain.Tasks
{
    public class Taska
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.New;

    }

    public enum TaskStatus
    {
        New,
        Cancelled,
        Completed
    }

}
