using Airport.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Airport.data
{
    public class SqlightContext : DbContext
    {
        public SqlightContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=.\db\airport.db");
        }
        public DbSet<PlaneData> Planes { get; set; }
        public DbSet<PlaneRecord> Records { get; set; }
    }
}
