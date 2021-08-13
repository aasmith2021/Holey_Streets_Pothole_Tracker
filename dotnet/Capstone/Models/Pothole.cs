using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Pothole
    {
        public int Id { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string? ImageLink { get; set; }

        public DateTime ReportedDate { get; set; }

        public int ReportingUserId { get; set; }

        public DateTime? InspectedDate { get; set; }

        public DateTime? RepairedDate { get; set; }

        public string RepairStatus { get; set; }

        public int Severity { get; set; }
    }
}
