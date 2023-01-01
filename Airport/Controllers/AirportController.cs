using Airport.Hubs;
using Airport.Models;
using Airport.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

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

        [HttpGet("/init")]
        public async Task<IActionResult> Init()
        {
            var tmp = await _airportLogic.GetStatus();
            if (!tmp.IsActive)
            {
                _airportLogic.Start();
                return Ok("inited");
            } 
            return Ok("already initialized");
        }
        [HttpGet("/land/{name}")]
        public void Land(string name)
        {
            Plane p = new();
            p.PlaneName = name;
            p.Destination = "land";
            _airportLogic.Land(p);
        }

        [HttpGet("/takeoff/{name}")]
        public void TakeOff(string name)
        {
            Plane p = new()
            {
                PlaneName = name,
                Destination = "takeOff"
            };
            _airportLogic.TakeOff(p);
        }

        [HttpGet("/current-state")]
        public ActionResult<List<StationState>> GetCurrentState() {
            Response.Headers.Add("X-header-empty", "");
            return Ok(_airportLogic.GetCurrentState());
        }
    }
}
