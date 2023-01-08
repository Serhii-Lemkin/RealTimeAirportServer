

using Airport.Models;

namespace Airport.Repositories
{
    public interface IPlaneHistoryRepository
    {
        public void AddPlane(string name, string destination);
        public void AddRecord(Plane p);
        public List<PlaneData> GetAllPlanes();
        public List<PlaneRecord> GetAllRecords();
        public List<PlaneRecord> GetRecords(string name);
    }
}
