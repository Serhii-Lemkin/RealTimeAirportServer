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
        List<ITakeOff> _takeoffs = new();

        private readonly IControllTower _controllTower;

        public AirportLogic(IControllTower controllTower)
        {
            _controllTower = controllTower;
        }

        public AirportStatus GetStatus()
        {
            return new AirportStatus
            {
                IsActive = _isActive,
                Landings = _landings,
                TakeOffs = _takeoffs
            };
        }

        public void Start()
        {

        }

    }
}
