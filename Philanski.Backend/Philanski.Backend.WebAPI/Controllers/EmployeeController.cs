using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Philanski.Backend.Library.Models;
using Philanski.Backend.Library.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Philanski.Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : Controller
    {

        public IRepository Repo { get; }

        public EmployeeController(IRepository repo)
        {
            Repo = repo;
        }
        // GET: api/<controller>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<List<Employee>> GetAll()
        {
            List<Employee> employees = Repo.GetAllEmployees();
            //catch null and send 404
            if (!employees.Any())
            {
                return NotFound();
            }
            List<Employee> singleEmployee = new List<Employee>();
            singleEmployee = (employees.Where(x => x.Email == User.Identity.Name).ToList());
            return singleEmployee;
        }

        // GET api/<controller>/{username}
        [HttpGet("{id}", Name = "GetEmployee")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Employee>> Get(string id)
        {
            //  var employee = await Repo.GetEmployeeByID(id);
            var employee = await Repo.GetEmployeeByEmail(id);
            //catch null and send 404
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }
        [HttpGet("{id}/timesheet")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<TimeSheet>>> GetTimeSheetsOfEmployee (string id)
        {
            var EmployeeId = await Repo.GetEmployeeIDByEmail(id);
            if (EmployeeId == 0)
            {
                return NotFound();
            }
            List<TimeSheet> TimeSheets = Repo.GetTimeSheetsByEmployeeId(EmployeeId);
            //catch null and send 404
            if (!TimeSheets.Any())
            {
                return NotFound();
            }
            return TimeSheets;
        }

        [HttpGet("{id}/timesheet/{weekstart}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<TimeSheet>>> GetFullWeek(string id, DateTime weekstart)
        {
            //NEED TO CHANGE THIS LATER BEACUSE IT IS NOT RESTFUL
            var EmployeeId = await Repo.GetEmployeeIDByEmail(id);
            if (EmployeeId == 0)
            {
                return NotFound();
            }
            List<TimeSheet> TimeSheets = Repo.GetEmployeeTimeSheetWeekFromDate(weekstart, EmployeeId);
            //catch null and send 404
            if (!TimeSheets.Any())
            {
                return NotFound();
            }
            return TimeSheets;
        }



        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
