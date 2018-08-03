using System;
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
    public class ManagerController : Controller
    {

        public IRepository Repo { get; }

        public ManagerController(IRepository repo)
        {
            Repo = repo;
        }
        // GET: api/<controller>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<List<Manager>> GetAll()
        {
            List<Manager> managers = Repo.GetAllManagers();
            //catch null and throw 404
            if (!managers.Any())
            {
                return NotFound();
            }
            return managers;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Manager>> Get(int id)
        {
            Manager manager = await Repo.GetManagerById(id);
            //catch null and throw 404
            if (manager == null)
            {
                return NotFound();
            }
            return manager;
        }

        [HttpGet("{id}/TimeSheetApproval")]
        public async Task<ActionResult<List<TimeSheetApproval>>> GetAllTimeSheetApprovalByManagerId(int id)
        {
            var TSAByManager = await Repo.GetAllTSAsThatCanBeApprovedByManager(id);
            if (!TSAByManager.Any())
            {
                return NotFound();
            }
            return TSAByManager;
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
