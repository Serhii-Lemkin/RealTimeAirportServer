using Airport.Models;

namespace Airport.Repository.Interface
{
    public interface IPlaneRepository
    {
        public IEnumerable<Plane> GetPlanes();
        public Plane GetPlane(string name);
        public void AddPlane(Plane p);
        public void DeletePlane(Plane p);
        public void DeletePlane(string name);
        public void UpdatePlane(Plane p);

    }
}
