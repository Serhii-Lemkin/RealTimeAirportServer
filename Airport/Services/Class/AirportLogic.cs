using Airport.Models;
using Airport.Models.Landings;
using Airport.Models.Takeoffs;
using Airport.Services.Interface;
using AirportProjct.Models;

namespace Airport.Services.Class
{
    public class AirportLogic : IAirportLogic
    {
        bool _isActive = false;
        List<ILanding> _landings = new();
        List<ITakeingOff> _takeoffs = new();

        private readonly IControllTower _controllTower;

        public AirportLogic(IControllTower controllTower)
        {
            _controllTower = controllTower;
            _controllTower.CheckState();
        }

        public async Task<AirportStatus> GetStatus()
        {
            return new AirportStatus
            {
                IsActive = _isActive,
                Landings = _landings,
                TakeOffs = _takeoffs
            };
        }

        public void Start() => _isActive = true;
        public void Stop() => _isActive = false;

        public async Task Land(Plane plane)
        {
            var route = _controllTower.GetRoute(plane.Destination);
            if (route == null) return;
            var landing = new Landing(plane, route);
            _landings.Add(landing);
            await landing.Land();
        }

        public async Task TakeOff(Plane plane)
        {
            var route = _controllTower.GetRoute(plane.Destination);
            if (route == null) return;
            var takingOff = new TakingOff(plane, route);
            _takeoffs.Add(takingOff);
            await takingOff.TakeOff();
        }

        public List<StationState> GetCurrentState()
        {
            return _controllTower.GetCurrentState();
        }
    }
}
