using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Philanski.Backend.Library.Models;
using Philanski.Backend.Library.Repositories;


namespace Philanski.Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {


        public Repository Repo { get; }

        public ValuesController(Repository repo)
        {
            Repo = repo;
        }
        // GET api/values 
        //test code
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

        /*    string nametest = Repo.testGetFirstEmployee();
            //return new string[] { "value1", "value2", nametest};
            DateTime today = DateTime.Today;
            var TSA = new TimeSheetApproval();
            DateTime weekStart = TSA.GetPreviousSundayOfWeek(today);
            int idTest = Repo.GetTimeSheetIdByDateAndEmpId(weekStart.AddDays(1), 1);*/
            return new string[] { "Please work" };
        }


        // GET api/values/5
       /* [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }*/

        // POST api/values for departments
     

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
