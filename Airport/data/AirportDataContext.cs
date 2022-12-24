using Airport.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Airport.data
{
    public class AirportDataContext : DbContext
    {
        public AirportDataContext(DbContextOptions<AirportDataContext> options) : base(options)
        {

        }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Plane> Planes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Station>().HasData(
                new Station { StationId = 1, StationName= "Landing Stage 1" },
                new Station{ StationId = 2, StationName = "Landing Stage 2" },
                new Station{ StationId = 3, StationName = "Landing Stage 3" },
                new Station{ StationId = 4, StationName = "Runway" },
                new Station{ StationId = 5, StationName = "Arrival Path" },
                new Station{ StationId = 6, StationName = "Terminal 1" },
                new Station{ StationId = 7, StationName = "Terminal 2" },
                new Station{ StationId = 8, StationName = "Departure Path" },
                new Station{ StationId = 9, StationName = "Take Off" }
                );
        }


    }
}
