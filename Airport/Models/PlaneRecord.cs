using Microsoft.EntityFrameworkCore;

namespace Airport.Models
{
    [PrimaryKey(nameof(RecordId))]
    public class PlaneRecord
    {
        public int RecordId { get; set; }
        public string PlaneName { get; set; }
        public string Destination { get; set; }
        public bool Finished { get; set; }
        public string? CurrentStation { get; set; }
        public DateTime? TimeOfAction { get; set; }
    }
}
