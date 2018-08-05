using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Philanski.Backend.Library.Models;
using Philanski.Backend.Library.Repositories;
using Microsoft.AspNetCore.Http;

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
        [AllowAnonymous]
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

        [HttpGet("{id}/timesheet/{weekstart}", Name = "EmployeeTimesheets")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<TimeSheet>>> GetFullWeek(string id, string weekstart)
        {
            var EmployeeId = await Repo.GetEmployeeIDByEmail(id);
            if (EmployeeId == 0)
            {
                return NotFound();
            }
            try
            {

                DateTime WeekStartDt = DateTime.ParseExact(weekstart, "dd-MM-yyyy",null);
                var actualWeekStart = TimeSheetApproval.GetPreviousSundayOfWeek(WeekStartDt);
                List<TimeSheet> TimeSheets = Repo.GetEmployeeTimeSheetWeekFromDate(actualWeekStart, EmployeeId);
                //catch null and send 404
                //if (!TimeSheets.Any())
                //{
                //    return NotFound();
                //}
                return TimeSheets;
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }

        [HttpGet("{id}/timesheetapproval")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<List<TimeSheetApproval>> GetAllTSAsByEmployeeUserName(string id)
        {

            int userid = await Repo.GetEmployeeIDByEmail(id);
            List <TimeSheetApproval> empTSAs = Repo.GetAllTimeSheetsFromEmployee(userid);
            return empTSAs;

        }



        [HttpGet("{id}/timesheetapproval/{weekstart}", Name = "EmployeeTimeSheetApproval")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<TimeSheetApproval>> GetTimeSheetApprovalByWeekStart(string id, string weekstart)
        {
            var EmployeeId = await Repo.GetEmployeeIDByEmail(id);
            if (EmployeeId == 0)
            {
                return NotFound();
            }
            try
            {

                
                DateTime DateWeekStart = DateTime.ParseExact(weekstart, "dd-MM-yyyy", null);

                //YALL FORGOT THIS PART AS IN ABOVE - Phil
                var actualWeekStart = TimeSheetApproval.GetPreviousSundayOfWeek(DateWeekStart);

                var TSA = await Repo.GetTimeSheetApprovalByEmployeeIdAndWeekStart(EmployeeId, actualWeekStart.Date);
                //we need to send something back to front end to represent they dont have one for that week
                return TSA;

                

            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        // POST api/<controller>
        //this will take in datetime.now and will have to find first day of the week
        [HttpPost("{id}/timesheet")]
        public async Task<ActionResult> PostFullWeek(string id, List<TimeSheet> timeSheets)
        {
            try
            {
                var EmployeeId = await Repo.GetEmployeeIDByEmail(id);
                if (EmployeeId == 0)
                {
                    return NotFound();
                }
                // var actualWeekStart = TimeSheetApproval.GetPreviousSundayOfWeek(weekstart);
                foreach (var timesheet in timeSheets)
                {
                    timesheet.EmployeeId = EmployeeId;
                    Repo.CreateTimeSheet(timesheet);

                }
                await Repo.Save();
                var weekStart = TimeSheetApproval.GetPreviousSundayOfWeek(timeSheets.ElementAt(0).Date).ToString("dd-MM-yyyy");
                return CreatedAtRoute("EmployeeTimesheets", new { weekstart = weekStart }, timeSheets);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
 
        }

        [HttpPost("{id}/timesheetapproval")]
        public async Task<ActionResult> PostTSA(string id, TimeSheetApproval TSA)
        {
            try
            {
                var EmployeeId = await Repo.GetEmployeeIDByEmail(id);
                if (EmployeeId == 0)
                {
                    return NotFound();
                }
                var TSATest = await Repo.GetTimeSheetApprovalByEmployeeIdAndWeekStart(EmployeeId, TSA.WeekStart);
                if (TSATest != null)
                {
                    return Conflict();
                }
                TSA.EmployeeId = EmployeeId;
                Repo.CreateTimeSheetApproval(TSA);
                await Repo.Save();
                return CreatedAtRoute("EmployeeTimeSheetApproval", new { weekstart = TSA.WeekStart }, TSA);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}/timesheet/{weekstart}")]
        public async Task<ActionResult> PutFullWeek(string id, string weekstart, List<TimeSheet> timeSheets)
        {
            var EmployeeId = await Repo.GetEmployeeIDByEmail(id);
            if (EmployeeId == 0)
            {
                return NotFound();
            }
            foreach (var timesheet in timeSheets)
            {
                timesheet.EmployeeId = EmployeeId;
                Repo.UpdateTimeSheet(timesheet);
            }
            await Repo.Save();
            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
