using Airport.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Airport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly IAirportLogic _airportLogic;
        public AirportController(IAirportLogic airportLogic)
        {
            _airportLogic = airportLogic;
        }

    }
}
