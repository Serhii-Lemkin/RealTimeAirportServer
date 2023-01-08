using Airport.Models;
using AirportProjct.Models;

namespace Airport.Services.Interface
{
    public interface IAirportLogic
    {
        public Task Land(Plane plane);
        public Task TakeOff(Plane plane);
        public List<StationState> GetCurrentState();
        public List<Plane> GetTakeOffs();
        public List<Plane> GetLandings();
        public StationState GetCurrentState(string stationName);
    }
}
