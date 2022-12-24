using Airport.data;
using Airport.Models;
using Airport.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Airport.Repository.Class
{
    public class PlaneRepository : IPlaneRepository
    {
        public PlaneRepository(AirportDataContext context) => _context = context;
        AirportDataContext _context { get; }

        public IEnumerable<Plane> GetPlanes() => _context.Planes!.Include(x => x.CurrentStation);

        public Plane GetPlane(string name) => _context.Planes!.Include(x => x.CurrentStation).FirstOrDefault(x => x.PlaneName == name)!;

        public void AddPlane(Plane p)
        {
            _context.Planes.Add(p);
            _context.SaveChanges();
        }

        public void DeletePlane(Plane p)
        {
            _context.Planes.Remove(p);
            _context.SaveChanges();
        }
        public void DeletePlane(string name)
        {
            var p = GetPlane(name);
            DeletePlane(p);
        }
        public void UpdatePlane(Plane p)
        {
            _context.Update(p);
        }

    }
}
