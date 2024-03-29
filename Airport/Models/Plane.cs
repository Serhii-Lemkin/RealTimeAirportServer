﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Airport.Models
{
    [PrimaryKey(nameof(PlaneName))]
    public class Plane
    {
        public string PlaneName { get; set; }
        public string Destination { get; set; }
        public bool Finished { get; set; }
        public string? CurrentStation { get; set; }
        public DateTime? TimeOfAction { get; set; }
    }
}
