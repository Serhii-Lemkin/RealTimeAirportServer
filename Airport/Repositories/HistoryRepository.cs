using Airport.data;
using Airport.Hubs;
using Airport.Models;
using Microsoft.AspNetCore.SignalR;

namespace Airport.Repositories
{
    public class PlaneHistoryRepository : IPlaneHistoryRepository
    {
        SqlightContext context = new();

        public void AddPlane(string name, string destination)
        {
            context.Planes.Add(new PlaneData { Destination = destination, PlaneName = name, DateCreated = DateTime.Now.ToUniversalTime() }) ;
            context.SaveChanges();
        }

        public void AddRecord(Plane p)
        {
            context.Records.Add(new PlaneRecord
            {
                PlaneName= p.PlaneName,
                CurrentStation=p.CurrentStation,
                Destination=p.Destination,
                Finished=p.Finished,
                TimeOfAction=p.TimeOfAction,
            });
            context.SaveChanges();
        }

        public List<PlaneData> GetAllPlanes() => context.Planes.ToList();

        public List<PlaneRecord> GetAllRecords() => context.Records.ToList();

        public List<PlaneRecord> GetRecords(string name) => context.Records.Where(x => x.PlaneName == name).ToList();
    }
}
