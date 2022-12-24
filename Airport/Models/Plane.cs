using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Airport.Models
{
    [PrimaryKey(nameof(PlaneName))]
    public class Plane
    {
        public string PlaneName { get; set; }
        public string Destination { get; set; }
        [ForeignKey(nameof(StationId))]
        public Station? CurrentStation { get; set; }
        public int? StationId { get; set; }
    }
}
