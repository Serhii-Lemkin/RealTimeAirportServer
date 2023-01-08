using Microsoft.EntityFrameworkCore;

namespace Airport.Models
{
        [PrimaryKey(nameof(Id))]
    public class PlaneData
    {
        public int Id { get; set; }    
        public string PlaneName { get; set; }
        public string Destination { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
