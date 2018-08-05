using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Philanski.Backend.Library.Models;
using Philanski.Backend.Library.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Philanski.Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {

        public IRepository Repo { get; }

        public DepartmentController(IRepository repo)
        {
            Repo = repo;
        }


        //response that gathers all departments

        
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin")]
        public ActionResult<List<Department>> GetAll()
        {
            List<Department> Departments = Repo.GetAllDepartments();
            //catch null and send 404
            if (!Departments.Any())
            {
                return NotFound();
            }

                return Departments;
            
        }
        
        //response that gathers a department by id
        //will route to /api/department/id
        [HttpGet("{id}", Name = "GetDepartment")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize]
        public async Task<ActionResult<Department>> GetById(int id)
        {
            var dept = await Repo.GetDepartmentByID(id);
            if (dept == null)
            {
                return NotFound();
            }
            return dept;
        }



            //creates a department
        [HttpPost]
        [ProducesResponseType(201)]
       // [ProducesResponseType(409)]
        public async Task<IActionResult> Post(Department department)
        {
            //check to see if department already exists (fix)
            Repo.CreateDepartment(department);
            await Repo.Save();
            department.Id = await Repo.GetDepartmentIdByName(department.Name);

            return CreatedAtRoute("GetDepartment", new { id = department.Id }, department);
        }


        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put(int id, Department department)
        {
            var dept = await Repo.GetDepartmentByID(id);
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
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
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
