using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Philanski.Backend.DataContext.Models;
using Philanski.Backend.Library.Models;
using Philanski.Backend.Library.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Philanski.Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSheetApprovalController : Controller
    {
        public IRepository Repo { get; }

        public TimeSheetApprovalController(IRepository repo)
        {
            Repo = repo;
        }

        // GET: api/<controller>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<List<TimeSheetApproval>> GetAll()
        {
           List<TimeSheetApproval> TimeSheetApprovals = Repo.GetAllTimeSheetApprovals();
            //catch null and send 404
            if (!TimeSheetApprovals.Any())
            {
                return NotFound();
            }
           return TimeSheetApprovals;
        }

        // GET api/<controller>/5
        [HttpGet("{id}", Name = "GetTimeSheetApproval")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<TimeSheetApproval>> Get(int id)
        {
            var TimeSheetApproval = await Repo.GetTimeSheetApprovalById(id);
            //catch null and send 404
            if (TimeSheetApproval == null)
            {
                return NotFound();
            }
            return TimeSheetApproval;
        }

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Post(TimeSheetApproval TSA)
        {
            //add check to see if TSA exists and throw 409 (fix)
            Repo.CreateTimeSheetApproval(TSA);
            await Repo.Save();


            TSA.Id = await Repo.GetTimeSheetApprovalIdByDateSubmitted(TSA.TimeSubmitted);

            //describes the route TSA is created at.

            return CreatedAtRoute("GetTimeSheetApproval", new { id = TSA.Id }, TSA);         
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
