using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Airport.Models
{
    public class Plane
    {
        public string PlaneName { get; set; }
        public string Destination { get; set; }
    }
}
