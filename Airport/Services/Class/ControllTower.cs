using Airport.Models;
using Airport.Repository.Interface;
using Airport.Services.Interface;

namespace Airport.Services.Class
{
    public class ControllTower : IControllTower
    {
        private readonly PlaneRoute LandingRoute;
        private readonly PlaneRoute TakeoffRoute;
        private readonly IStationRepository stations;

        public ControllTower(IStationRepository _stations)
        {
            stations = _stations;
            var tmp = stations.GetStations().ToArray();

            LandingRoute = new PlaneRoute(new List<Station>
            {
                tmp[0],
                tmp[1],
                tmp[2],
                tmp[3],
                tmp[4],
                tmp[5],
                tmp[6],
            });
            TakeoffRoute = new PlaneRoute(new List<Station>
            {
                tmp[5],
                tmp[6],
                tmp[7],
                tmp[3],
                tmp[8],
            });
        }
        public PlaneRoute GetRoute(string destination)
        {
            switch (destination)
            {
                case "landing": return LandingRoute;
                case "takingOff": return TakeoffRoute;
                default: return null;
            }
        }
    }
}
