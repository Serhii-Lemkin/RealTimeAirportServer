using Airport.Models.Landings;
using Airport.Models.Takeoffs;

namespace AirportProjct.Models
{
    public class AirportStatus
    {
        public bool IsActive { get; internal set; }
        public List<ILanding> Landings { get; internal set; }
        public List<ITakeingOff> TakeOffs { get; internal set; }
    }
}