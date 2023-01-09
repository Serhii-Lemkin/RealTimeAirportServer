using Airport.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.Mocks
{
    internal class MoqedModels
    {
        public Plane GetPlaneLanding()
        {
            return new Plane
            {
                PlaneName = "TestLanding",
                Destination = "land",
                TimeOfAction = DateTime.Now.ToUniversalTime()
            };
        }
        public Plane GetPlaneTakeOff()
        {
            return new Plane
            {
                PlaneName = "TestTakeOff",
                Destination = "takeOff",
                TimeOfAction = DateTime.Now.ToUniversalTime()
            };
        }
        public List<Station> GetSimpleRoute(IHubContext<Airport.Hubs.AirportHub> hub)
        {
            return new List<Station> {
                new Station(hub) { StationName = "testStation1", StationId = 1 },
                new Station(hub) { StationName = "testStation2", StationId = 2 },
            };
        }
    }
}
