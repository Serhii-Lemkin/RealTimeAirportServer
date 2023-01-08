using Airport.data;
using Airport.Hubs;
using Airport.Models;
using Airport.Repositories;
using Airport.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Airport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly IAirportLogic _airportLogic;
        private readonly IHubContext<AirportHub> hub;

        public AirportController(IAirportLogic airportLogic, IHubContext<AirportHub> hub)
        {
            _airportLogic = airportLogic;
            this.hub = hub;
        }

        [HttpGet("/land/{name}")]
        public void Land(string name)
        {
            Plane p = new();
            p.PlaneName = name;
            p.Destination = "land";
            p.TimeOfAction = DateTime.Now.ToUniversalTime();
            _airportLogic.Land(p);
        }

        [HttpGet("/takeoff/{name}")]
        public void TakeOff(string name)
        {
            Plane p = new()
            {
                TimeOfAction = DateTime.Now.ToUniversalTime(),
                PlaneName = name,
                Destination = "takeOff"
            };
            _airportLogic.TakeOff(p);
        }
        [HttpGet("current-state/{stationName}")]
        public ActionResult<List<StationState>> GetCurrentState(string stationName)
        {
            return Ok(_airportLogic.GetCurrentState(stationName));
        }

        [HttpGet("current-state")]
        public ActionResult<List<StationState>> GetCurrentState()
        {
            return Ok(_airportLogic.GetCurrentState());
        }
        [HttpGet("takeoffs")]
        public ActionResult<List<Plane>> GetTakeOffs()
        {
            return Ok(_airportLogic.GetTakeOffs());
        }
        [HttpGet("landings")]
        public ActionResult<List<Plane>> GetLandings()
        {
            return Ok(_airportLogic.GetLandings());
        }
    }
}
