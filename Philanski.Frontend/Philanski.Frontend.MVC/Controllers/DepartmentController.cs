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
    public class DepartmentController : Controller
    {

        public readonly static string ServiceUri = "https://philanski.azurewebsites.net/api/";

        public HttpClient HttpClient { get; }
        // GET: Department

        public DepartmentController(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
        public async Task<ActionResult> Index()
        {
            var uri = ServiceUri + "department";
            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            try
            {
                var response = await HttpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    return View("Error");
                }

                string jsonString = await response.Content.ReadAsStringAsync();

                List<Departments> dept = JsonConvert.DeserializeObject<List<Departments>>(jsonString);

                return View(dept);
            }
            catch (HttpRequestException ex)
            {
                // logging
                return View("Error");
            }
        }

        // GET: Department/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
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

        // GET: Department/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Department/Edit/5
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

        // GET: Department/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Department/Delete/5
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