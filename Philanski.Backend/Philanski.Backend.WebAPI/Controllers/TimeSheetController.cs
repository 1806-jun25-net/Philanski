﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Philanski.Backend.Library.Models;
using Philanski.Backend.Library.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Philanski.Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSheetController : Controller
    {
        public IRepository Repo { get; }

        public TimeSheetController(IRepository repo)
        {
            Repo = repo;
        }

        // GET: api/<controller>
        // get all time sheets. works
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<List<TimeSheet>> GetAll()
        {
            List<TimeSheet> TimeSheets = Repo.GetAllTimeSheets();
            //catch null and throw 404
            if (!TimeSheets.Any())
            {
                return NotFound();
            }
            return TimeSheets;

        }

        // GET api/<controller>/5
        [HttpGet("{id}", Name = "GetTimeSheet")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<TimeSheet>> GetById(int id)
        {
            var timesheet =  await Repo.GetTimeSheetByID(id);
            //catch null and throw 404
            if (timesheet == null)
            {
                return NotFound();
            }
            return timesheet;
        }

        [HttpGet("GetFullWeek")] //api/timesheet/GetFullWeek?username={email}&&date={date}
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<TimeSheet>>> GetFullWeek(string username, DateTime date)
        {
            //NEED TO CHANGE THIS LATER BEACUSE IT IS NOT RESTFUL
            var EmployeeId = await Repo.GetEmployeeIDByEmail(username);
            if (EmployeeId == 0)
            {
                return NotFound();
            }
            List<TimeSheet> TimeSheets = Repo.GetEmployeeTimeSheetWeekFromDate(date, EmployeeId);
            //catch null and send 404
            if (!TimeSheets.Any())
            {
                return NotFound();
            }
            return TimeSheets;
        }

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Post(TimeSheet timesheet)
        {
            //check that timesheets will have ID?
            //check db if it already exists (fix)
            var DoesIdExist = await Repo.GetTimeSheetIdByDateAndEmpId(timesheet.Date, timesheet.EmployeeId);
            if (DoesIdExist != 0)
            {
                return Conflict();
            }

            //Create time sheet
            Repo.CreateTimeSheet(timesheet);
            await Repo.Save();
            timesheet.Id = await Repo.GetTimeSheetIdByDateAndEmpId(timesheet.Date, timesheet.EmployeeId);
            //describes route it is save too
            return CreatedAtRoute("GetTimeSheet", new { id = timesheet.Id }, timesheet);
        }

        // PUT api/<controller>/5
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TimeSheet UpdatedTimeSheet)
        {
            var TimeSheet = await Repo.GetTimeSheetByID(id);
            if (TimeSheet == null)
            {
                return NotFound();
            }
            Repo.UpdateTimeSheet(UpdatedTimeSheet);
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
