using Airport.Hubs;
using Airport.Models;
using Airport.Models.Landings;
using Airport.Models.Takeoffs;
using Airport.Repositories;
using Airport.Services.Interface;
using AirportProjct.Models;
using Microsoft.AspNetCore.SignalR;

namespace Airport.Services.Class
{
    public class AirportLogic : IAirportLogic
    {
        bool _isActive = false;
        List<ILanding> _landings = new();
        List<ITakingOff> _takeoffs = new();

        private readonly IControllTower _controllTower;
        private readonly IHubContext<AirportHub> hub;
        private readonly IPlaneHistoryRepository history;

        public AirportLogic(IControllTower controllTower, IHubContext<AirportHub> hub, IPlaneHistoryRepository history)
        {
            _controllTower = controllTower;
            this.hub = hub;
            this.history = history;
            _controllTower.CheckState();

        }
        public async Task Land(Plane plane)
        {
            var route = _controllTower.GetRoute(plane.Destination);
            if (route == null) return;
            var landing = new Landing(plane, route, hub, history);
            _landings.Add(landing);
            history.AddPlane(plane.PlaneName, plane.Destination);
            await landing.Land();
        }

        public async Task TakeOff(Plane plane)
        {
            var route = _controllTower.GetRoute(plane.Destination);
            if (route == null) return;
            var takingOff = new TakingOff(plane, route, hub, history);
            _takeoffs.Add(takingOff);
            history.AddPlane(plane.PlaneName, plane.Destination);
            await takingOff.TakeOff();
        }

        public List<StationState> GetCurrentState() => _controllTower.GetCurrentState();

        public StationState GetCurrentState(string stationName) => _controllTower.GetCurrentState().FirstOrDefault(x => x.StationName == stationName)!;

        public List<Plane> GetTakeOffs()
        {
            var takeoffs = new List<Plane>();
            foreach (var takeoff in _takeoffs)
            {
                if (takeoff.GetPlane().Finished == false) takeoffs.Add(takeoff.GetPlane());
            }
            return takeoffs;
        }

        public List<Plane> GetLandings()
        {
            var landings = new List<Plane>();
            foreach (var landing in _landings)
            {
                if (landing.GetPlane().Finished == false) landings.Add(landing.GetPlane());
            }
            return landings;
        }
    }
}
