using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IClaimFormDao
    {
        public List<ClaimForm> GetAllClaims();
        public bool addClaimForm(ClaimForm newClaim);

        public bool UpdateClaim(ClaimForm claimForm);
    }
}
