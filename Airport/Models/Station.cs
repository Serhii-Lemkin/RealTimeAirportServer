using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Airport.Models
{
    [PrimaryKey(nameof(StationId))]
    public class Station
    {
        public int StationId { get; set; }
        [ForeignKey(nameof(PlaneName))]
        public Plane? CurrentPlane { get; set; }
        public string? PlaneName { get; set; }
        public string? StationName { get; set; }

    }
}
