using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Models;
using Capstone.Security;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClaimFormController : ControllerBase
    {
        private readonly IClaimFormDao claimFormDao;

        public ClaimFormController(IClaimFormDao _claimFormDao)
        {
            claimFormDao = _claimFormDao;
        }

        //route = /claimform
        [HttpGet]
        [Authorize(Roles="admin")]
        public IActionResult getAllClaimForms()
        {
            List<ClaimForm> allClaimForms = claimFormDao.GetAllClaims();

            if (allClaimForms != null)
            {
                return Ok(allClaimForms);
            }
            else
            {
                return StatusCode(500);
            }
        }

        //route = /claimform/submit
        [HttpPost("submit")]
        [Authorize]
        public IActionResult addNewClaim(ClaimForm newForm)
        { 
            newForm.UserId = int.Parse(User.FindFirst("sub")?.Value);

            bool addSuccessful = claimFormDao.addClaimForm(newForm);

            if (addSuccessful)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }
        }
        [HttpPut]
        [Authorize]
        public IActionResult updateClaim(ClaimForm claimToUpdate)
        {
            bool addSuccessful = claimFormDao.UpdateClaim(claimToUpdate);

            if (addSuccessful)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}
