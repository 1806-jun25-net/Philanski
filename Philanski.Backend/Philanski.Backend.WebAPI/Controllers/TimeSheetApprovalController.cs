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
        public Repository Repo { get; }
    
        public TimeSheetApprovalController(Repository repo)
        {
            Repo = repo;
        }


        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<TimeSheetApproval>> GetAll()
        {
           List<TimeSheetApproval> TimeSheetApprovals = Repo.GetAllTimeSheetApprovals();
           return TimeSheetApprovals;
        }

        // GET api/<controller>/5
        [HttpGet("{id}", Name = "GetTimeSheetApproval")]
        public ActionResult<TimeSheetApproval> Get(int id)
        {
            var TimeSheetApproval = Repo.GetTimeSheetApprovalById(id);
            //catch null and send 404
            if (TimeSheetApproval == null)
            {
                return NotFound();
            }
            return TimeSheetApproval;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(TimeSheetApproval TSA)
        {
            Repo.CreateTimeSheetApproval(TSA);
            await Repo.Save();
            //TSA.Id = Repo.GetTimeSheetApprovalIdByDateSubmitted(TSA.TimeSubmitted);
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
