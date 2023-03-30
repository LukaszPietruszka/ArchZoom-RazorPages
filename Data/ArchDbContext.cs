using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public class ArchDbContext : DbContext
    {
        public ArchDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ArchModel> archModels { get; set; }
    }
}
