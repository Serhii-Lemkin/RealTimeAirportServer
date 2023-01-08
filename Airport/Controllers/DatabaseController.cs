using Airport.Models;
using Airport.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Airport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly IPlaneHistoryRepository history;

        public DatabaseController(IPlaneHistoryRepository rep)
        {
            this.history = rep;
        }
        [HttpGet("planes")]
        public ActionResult<List<StationState>> getPlanes()
        {
            return Ok(history.GetAllPlanes());
        }
        [HttpGet("records/{name}")]
        public ActionResult<List<StationState>> GetRecords(string name)
        {
            return Ok(history.GetRecords(name));
        }
    }
}
