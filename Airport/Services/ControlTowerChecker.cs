using Airport.Models;

namespace Airport.Services
{
    public class ControlTowerChecker
    {
        public List<Station> AllStations{ get; set; }
        public ControlTowerChecker(List<Station> allStations)
        {
            AllStations = allStations;
        }

        public async Task<bool> CheckIfStopIncoming()
        {
            return await Task.Run(async () =>
            {
                return (AllStations[3].CurrentPlane != null && AllStations[3].CurrentPlane.Destination == "land") || AllStations[4].CurrentPlane != null;
            });
        }

        public async Task<bool> CheckClogged()
        {
            return await Task.Run(async () =>
            {
                return AllStations[5].CurrentPlane.Destination == "takeOff" &&
                            AllStations[6].CurrentPlane.Destination == "takeOff" &&
                            AllStations[7].CurrentPlane.Destination == "takeOff";
            });
        }

        public async Task<bool> CheckStopped()
        {
            return await Task.Run(async () =>
            {
                return  AllStations[5].CurrentPlane.Destination == "takeOff" &&
                                AllStations[3].CurrentPlane.Destination == "land" &&
                                AllStations[4].CurrentPlane.Destination == "land" &&
                                AllStations[6].CurrentPlane.Destination == "takeOff" &&
                                AllStations[7].CurrentPlane.Destination == "takeOff";
            });
        }

        public async Task<bool> CheckIfStopOutcoming()
        {
            return await Task.Run(async () =>
            {
                return AllStations[4].CurrentPlane != null && AllStations[3].CurrentPlane != null;
            });
        }
    }
}
