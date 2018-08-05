using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Philanski.Frontend.MVC.Models;

namespace Philanski.Frontend.MVC.Controllers
{
    public class EmployeeController : AServiceController
    {

        public EmployeeController(HttpClient httpClient) : base(httpClient)
        {

        }
        // GET: Employee
        public async Task<ActionResult> Index()
        {


     /*       if (TempData.Peek("username") == null)
            {
                return View("Forbidden");
            }*/
            var username = TempData.Peek("username");
            

            var request = CreateRequestToService(HttpMethod.Get, "api/Employee/" + username);


            try
            {
                var response = await HttpClient.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return View("Forbidden");
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return View("Unauthorized");
                }
                if (!response.IsSuccessStatusCode)
                {
                    return View("Whoops");
                }

                string jsonString = await response.Content.ReadAsStringAsync();

                Employees employee = JsonConvert.DeserializeObject<Employees>(jsonString);

                return View(employee);
            }
            catch (HttpRequestException ex)
            {
                return View("Whoops");
            }


           
        }

        // GET: Employee/Details/5
        public async Task<ActionResult> ViewTSAs()
        {



          // if (TempData.Peek("username") == null)
          //  {
          //      return View("Forbidden");
          //  }
            var username = TempData.Peek("username");


            var request = CreateRequestToService(HttpMethod.Get, "api/Employee/" + username + "/timesheetapproval");


            try
            {
                var response = await HttpClient.SendAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return View("Forbidden");
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return View("Unauthorized");
                }
                if (!response.IsSuccessStatusCode)
                {
                    return View("Whoops");
                }

                string jsonString = await response.Content.ReadAsStringAsync();

                List<TimeSheetApprovals> employeeTSAs = JsonConvert.DeserializeObject<List<TimeSheetApprovals>>(jsonString);

                return View(employeeTSAs);
            }
            catch (HttpRequestException ex)
            {
                return View("Whoops");
            }
           
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}