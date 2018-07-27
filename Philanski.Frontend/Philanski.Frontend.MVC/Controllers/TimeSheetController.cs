using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Philanski.Frontend.MVC.Models;

namespace Philanski.Frontend.MVC.Controllers
{
    public class TimeSheetController : Controller
    {
        public readonly static string ServiceUri = "https://localhost:44386/api/";

        public HttpClient HttpClient { get; }
        // GET: Department

        public TimeSheetController(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<ActionResult> Index()
        {
            //api/timesheet/GetFullWeek?EmployeeId=id&&date={date}

            var uri = ServiceUri + "timesheet/GetFullWeek?EmployeeId=1&&date="+DateTime.Now.Date;
            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            try
            {
                var response = await HttpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    return View("Error");
                }

                string jsonString = await response.Content.ReadAsStringAsync();

                List<TimeSheets> timeSheet = JsonConvert.DeserializeObject<List<TimeSheets>>(jsonString);

                return View(timeSheet);
            }
            catch (HttpRequestException ex)
            {
                // logging
                return View("Error");
            }
        }
    }
}