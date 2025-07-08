using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TaskManagement.Infra.Data.EF.SqlServer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Core.Domain.Tasks.Taska> Tasks { get; set; }


    }
}
