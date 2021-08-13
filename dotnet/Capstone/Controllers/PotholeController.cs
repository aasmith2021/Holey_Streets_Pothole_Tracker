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
    public class PotholeController : ControllerBase
    {
        private readonly IPotholeDao potholeDao;

        public PotholeController(IPotholeDao _potholeDao)
        {
            potholeDao = _potholeDao;
        }

        [HttpPost("add")]
        [Authorize]
        public IActionResult AddNewPothole(Pothole pothole)
        {
            pothole.ReportingUserId = int.Parse(User.FindFirst("sub")?.Value);
            pothole.ReportedDate = System.DateTime.Now.Date;
            pothole.RepairStatus = "Reported";
            bool addResult = potholeDao.AddPothole(pothole);
            if (addResult)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpGet("list")]
        public ActionResult<List<Pothole>> ListAllPotholes()
        {
            List<Pothole> allPotholes = potholeDao.ListPotholes();
            if (allPotholes != null)
            {
                return Ok(allPotholes);
            }
            else
            {
                return StatusCode(500);
            }
        }

        //URL: pothole/<potholeId>
        [HttpDelete("{potholeId}")]
        //[Authorize(Roles = "admin, employee")]
        public IActionResult DeletePothole(int potholeId)
        {
            bool deleteSuccessful = potholeDao.DeletePothole(potholeId);

            if (deleteSuccessful)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500);
            }
        }

        //connect to our update method
        [HttpPut]
        //[Authorize(Roles = "admin, employee")]
        public IActionResult UpdatePothole(Pothole pothole)
        {
            bool updateSuccessful = potholeDao.UpdatePothole(pothole);

            if (updateSuccessful)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}
