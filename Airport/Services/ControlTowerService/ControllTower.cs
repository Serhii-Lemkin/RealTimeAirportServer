using Airport.Hubs;
using Airport.Models;
using Airport.Repositories;
using Airport.Services.Interface;
using Microsoft.AspNetCore.SignalR;

namespace Airport.Services.ControlTower
{
    public class ControllTower : IControllTower
    {
        private readonly List<Station> AllStations;
        private readonly ControlTowerChecker ctChecker;

        public bool Stopped { get; private set; }
        public IHubContext<AirportHub> Hub { get; }

        public ControllTower(IHubContext<AirportHub> hub)
        {
            Hub = hub;
            AllStations = new List<Station>
            {
                new Station(Hub) { StationId = 1, StationName = "Landing Stage 1" },
                new Station(Hub) { StationId = 2, StationName = "Landing Stage 2" },
                new Station(Hub) { StationId = 3, StationName = "Landing Stage 3" },
                new Station(Hub) { StationId = 4, StationName = "Runway" },
                new Station(Hub) { StationId = 5, StationName = "Arrival Path" },
                new Station(Hub) { StationId = 6, StationName = "Terminal 1" },
                new Station(Hub) { StationId = 7, StationName = "Terminal 2" },
                new Station(Hub) { StationId = 8, StationName = "Departure Path" },
                new Station(Hub) { StationId = 9, StationName = "Take Off" }
            };
            Stopped = false;
            ctChecker = new(AllStations);

        }
        public Task CheckState()
        {
            return Task.Run(async () =>
            {
                var runway = AllStations[3];
                var arrivalPath = AllStations[4];
                var term1 = AllStations[5];
                var term2 = AllStations[6];
                var departurePath = AllStations[7];
                while (true)
                {
                    try
                    {
                        if (
                        term1.CurrentPlane != null &&
                        term2.CurrentPlane != null &&
                        departurePath.CurrentPlane != null)
                        {
                            if (await ctChecker.CheckIfStopOutcoming())
                            {
                                if (await ctChecker.CheckStopped())
                                {
                                    Stopped = true;
                                    Console.WriteLine(DateTime.Now);
                                    Console.WriteLine("_______________________________Airport has Stopped");
                                }

                            }
                            if (await ctChecker.CheckClogged())
                            {
                                runway.incomingDelayed = true;
                                Console.WriteLine("Airport is clogged");
                            }

                        }
                        else
                        {

                            runway.incomingDelayed = false;
                            Console.WriteLine("Airport is not clogged");
                        }
                        if (await ctChecker.CheckIfStopIncoming())
                        {
                            runway.incomingDelayed = true;
                            term1.outcomingDelayed = true;
                            Console.WriteLine("outcoming closed");
                        }
                        else
                        {
                            runway.incomingDelayed = false;
                            term1.outcomingDelayed = false;
                            Console.WriteLine("outcoming opened");
                        }
                        Console.WriteLine(".");
                        Thread.Sleep(500);

                    }
                    catch (Exception ex) { Console.WriteLine(ex); }
                }
                Console.WriteLine("why?");
            });
        }

        public PlaneRoute? GetRoute(string destination)
        {
            switch (destination)
            {
                case "land":
                    return new PlaneRoute
                    {
                        _stations = new List<Station>
                        {
                            AllStations[0],
                            AllStations[1],
                            AllStations[2],
                            AllStations[3],
                            AllStations[4],
                            AllStations[5],
                            AllStations[6],
                        }
                    };
                case "takeOff":
                    return new PlaneRoute
                    {
                        _stations = new List<Station>
                        {
                            AllStations[5],
                            AllStations[6],
                            AllStations[7],
                            AllStations[3],
                            AllStations[8],
                        }
                    };
                default: return null;
            }
        }

        public List<StationState> GetCurrentState()
        {

            var state = new List<StationState>();
            AllStations.ForEach(x =>
            {
                var station = new StationState();
                if (x.CurrentPlane != null) station.CurrentPlane = x.CurrentPlane;
                station.StationName = x.StationName;
                state.Add(station);
            });
            return state;
        }
    }
}
