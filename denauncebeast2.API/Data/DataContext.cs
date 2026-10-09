using denauncebeast2.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace denauncebeast2.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Sector> Sectors { get; set; }
    }
}
