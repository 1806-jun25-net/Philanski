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
    public class DepartmentController : Controller
    {

        public Repository Repo { get; }

        public DepartmentController(Repository repo)
        {
            Repo = repo;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
      /*  [HttpPost]
        public void Post([FromBody]string value)
        {
        }*/

        [HttpPost]
        public IActionResult Post(Department department)
        {
            Repo.CreateDepartment(department);
            Repo.Save();
            department.Id = Repo.GetDepartmentIdByName(department.Name);

            return CreatedAtRoute("Create", new { id = department.Id }, department);
        }

        [HttpGet("{id}", Name = "Create")]
        public ActionResult<Department> GetById(int id)
        {
            var item = Repo.GetDepartmentByID(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
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
