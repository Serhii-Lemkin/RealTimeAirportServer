using Airport.data;
using Airport.Models;
using Airport.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Airport.Repository.Class
{
    public class Stationrepository : IStationRepository
    {
        public Stationrepository(AirportDataContext context) => _context = context;
        AirportDataContext _context { get; }

        public Station GetStation(int id) => _context.Stations!.Include(x => x.CurrentPlane).FirstOrDefault(x => x.StationId == id)!;

        public IEnumerable<Station> GetStations() => _context.Stations!.Include(x => x.CurrentPlane);

        public void UpdateStation(Station station)
        {
            _context.Stations?.Update(station); 
            _context.SaveChanges();
        }
    }
}
