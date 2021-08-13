using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class ClaimForm
    {
        public int ClaimId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfClaim { get; set; }
        public DateTime DateOfIncident { get; set; }
        public string LocationOfIncidentCity { get; set; }
        public string LocationOfIncidentState { get; set; }
        public string ImageLink { get; set; }
        public string DescriptionOfDamage { get; set; }
        public string ClaimStatus { get; set; }
    }
}
