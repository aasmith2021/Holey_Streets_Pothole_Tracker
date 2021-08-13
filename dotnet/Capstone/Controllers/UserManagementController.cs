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
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagementDao userDao;

        public UserManagementController(IUserManagementDao _userDao)
        {
            userDao = _userDao;
        }

        //URL: .../usermanagement/{userId}?newrole=employee
        [HttpPut("{userId}")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateUserRole(int userId, string newRole)
        {
            bool updateSuccessful = userDao.ChangeUserRole(userId, newRole);

            if (updateSuccessful)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{role}")]
        [Authorize(Roles = "admin")]
        public ActionResult<List<ReturnUser>> ListAllUsersByRole(string role)
        {
            List<ReturnUser> allUsers = userDao.ListUsersByRole(role);
            
            if (allUsers != null)
            {
                return Ok(allUsers);
            }
            else
            {
                return StatusCode(500);
            }
        }

        //A user requests to get employee access
        //URL: .../usermanagement/request
        [HttpPost("request")]
        [Authorize]
        public IActionResult RequestEmployeeAccess()
        {
            ReturnUser user = new ReturnUser();
            
            user.UserId = int.Parse(User.FindFirst("sub")?.Value);
            bool noExistingRequests = userDao.CheckIfActiveRequest(user);
            if (noExistingRequests)
            {
                return StatusCode(409);
            }
            bool requestAdded = userDao.RequestEmployeeAccess(user);
            if (requestAdded)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }
        }

        //URL: .../usermanagement/request/{userId}?approved=true/false
        //The admin approves an access change request
        [HttpPut("request/{userId}")]
        [Authorize(Roles = "admin")]
        public IActionResult ApproveDenyEmployeeAccessRequest(int userId, bool approved)
        {
            bool roleChangeSuccessful = false;
            
            //This is the NEW USER profile we want to update the database to
            ReturnUser user = new ReturnUser();

            user.UserId = userId;
            user.Role = "user";
            
            //If their request to be an employee is approved, then their new role in the
            //database will be "employee"
            if (approved)
            {
                user.Role = "employee";

                roleChangeSuccessful = userDao.ChangeUserRole(user.UserId, user.Role);
            }
            else
            {
                roleChangeSuccessful = true;
            }
            
            bool requestApprovedOrDenied = userDao.SetRequestStatusToInactive(userId);

            if (roleChangeSuccessful && requestApprovedOrDenied)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }
        }

        //Gets all access change requests
        //URL: .../usermanagement/request
        [HttpGet("request")]
        [Authorize(Roles = "admin")]
        public ActionResult<List<AccessChangeRequest>> ListAllActiveChangeRequests()
        {
            List<AccessChangeRequest> allActiveChangeRequests = userDao.ListAllActiveChangeRequests();

            if (allActiveChangeRequests != null)
            {
                return Ok(allActiveChangeRequests);
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}
