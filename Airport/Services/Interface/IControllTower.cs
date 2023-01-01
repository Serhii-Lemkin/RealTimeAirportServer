using Airport.Models;

namespace Airport.Services.Interface
{
    public interface IControllTower
    {
        Task CheckState();
        public List<StationState> GetCurrentState();
        public PlaneRoute GetRoute(string destination);
    }
}
