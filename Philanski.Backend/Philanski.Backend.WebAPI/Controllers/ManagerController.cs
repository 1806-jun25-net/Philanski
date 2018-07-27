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

        public Repository Repo { get; }

        public ManagerController(Repository repo)
        {
            Repo = repo;
        }
        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<Manager>> GetAll()
        {
            List<Manager> managers = Repo.GetAllManagers();
            if (managers == null)
            {
                return NotFound();
            }
            return managers;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<Manager> Get(int id)
        {
            Manager manager = Repo.GetManagerById(id);
            if (manager == null)
            {
                return NotFound();
            }
            return manager;
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
