using Airport.Models;
using AirportProjct.Models;

namespace Airport.Services.Interface
{
    public interface IAirportLogic
    {
        public Task<AirportStatus> GetStatus();
        public void Start();
        public void Stop();
        public Task Land(Plane plane);
        public Task TakeOff(Plane plane);
        public List<StationState> GetCurrentState();
        public List<Plane> GetTakeOffs();
        public List<Plane> GetLandings();
    }
}
