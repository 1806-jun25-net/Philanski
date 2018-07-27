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
    public class DepartmentController : Controller
    {

        public Repository Repo { get; }

        public DepartmentController(Repository repo)
        {
            Repo = repo;
        }
        // GET: api/<controller>
       /* [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

        //response that gathers all departments
        [HttpGet]
        public ActionResult<List<Department>> GetAll()
        {
            List<Department> Departments = Repo.GetAllDepartments();
            return Departments;
        }
        
        //response that gathers a department by id
        //will route to /api/department/id
        [HttpGet("{id}", Name = "GetDepartment")]
        public ActionResult<Department> GetById(int id)
        {
            var dept = Repo.GetDepartmentByID(id);
            if (dept == null)
            {
                return NotFound();
            }
            return dept;
        }



            //creates a department
        [HttpPost]
        public async Task<IActionResult> Post(Department department)
        {
            //check to see if department already exists (fix)
            Repo.CreateDepartment(department);
            await Repo.Save();
            department.Id = Repo.GetDepartmentIdByName(department.Name);

            return CreatedAtRoute("GetDepartment", new { id = department.Id }, department);
        }


        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Department department)
        {
            var dept = Repo.GetDepartmentByID(id);
            if (dept == null)
            {
                return NotFound();
            }
            //might need to add ID here. UPDATE: I put id handling in repo. We will need to set it to update it
            dept.Name = department.Name;
            Repo.UpdateDepartment(dept);
            await Repo.Save();
            return NoContent();

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dept = Repo.GetDepartmentByID(id);
            if (dept == null)
            {
                return NotFound();
            }
            Repo.DeleteDepartment(id);
            await Repo.Save();
            return NoContent();
        }
    }
}
