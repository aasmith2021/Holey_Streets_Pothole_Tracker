using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class AccessChangeRequest
    {
        public int RequestId { get; set; }

        public int UserId { get; set; }

        public string Username { get; set; }

        public string Role { get; set; }

        public bool ActiveStatus { get; set; }
    }
}
