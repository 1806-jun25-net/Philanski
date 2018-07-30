using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Philanski.Frontend.MVC.Models;

namespace Philanski.Frontend.MVC.Controllers
{
    public class TimeSheetController : Controller
    {
        public readonly static string ServiceUri = "https://philanksi.azurewebsites.net/api/";

        public HttpClient HttpClient { get; }
        // GET: Department

        public TimeSheetController(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<ActionResult> Index()
        {

            //get employeeId from Identity (not the same as employeeId in DB)
            //var claimsIdentity = User.Identity as ClaimsIdentity;
            //if (claimsIdentity != null)
            //{
            //    // the principal identity is a claims identity.
            //    // now we need to find the NameIdentifier claim
            //    var userIdClaim = claimsIdentity.Claims
            //        .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            //    if (userIdClaim != null)
            //    {
            //        var employeeId = userIdClaim.Value;
            //    }
            //}

            //get employeeId from DB based on Identity employeeId
            
            var uri = ServiceUri + "employee/1/timesheet";
            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            try
            {
                //get time sheets belonging to logged in employee
                var response = await HttpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    return View("Whoops");
                }

                string jsonString = await response.Content.ReadAsStringAsync();

                List<TimeSheets> timeSheet = JsonConvert.DeserializeObject<List<TimeSheets>>(jsonString);

                //sort timeSheet by date monday-friday
                timeSheet = timeSheet.OrderByDescending(x => x.Date).ToList();

                List<TimeSheets>[] ArrayOfListOfWeeks = new List<TimeSheets>[timeSheet.Count / 7];
                //organize timesheets into weeks in 2D array
                for (int i = 0; i<(timeSheet.Count/7); i++)
                {
                    List<TimeSheets> timeSheetWeeks = new List<TimeSheets>();
                    for(int j = 0; j<7; j++)
                    {
                        timeSheetWeeks.Add(timeSheet.ElementAt((i * 7) + j));
                    }
                    ArrayOfListOfWeeks[i] = timeSheetWeeks; //ArrayOfListsOfWeeks[0].ElementAt(0);
                }

                //ArrayOfListOfWeeks is an Array of Lists (7 by x) containting all the time sheets (by week) ordered by date (most recent first)
                return View(ArrayOfListOfWeeks);
            }
            catch (HttpRequestException ex)
            {
                // logging
                return View("Error");
            }
        }

        public async Task<ActionResult> Create()
        {
            //api/timesheet/GetFullWeek?EmployeeId=id&&date={date}

            var uri = ServiceUri + "timesheet/GetFullWeek?EmployeeId=1&&date=" + DateTime.Now.Date;
            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            try
            {
                var response = await HttpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    return View("Whoops");
                }

                string jsonString = await response.Content.ReadAsStringAsync();

                List<TimeSheets> timeSheet = JsonConvert.DeserializeObject<List<TimeSheets>>(jsonString);

                //sort timeSheet by date monday-friday
                timeSheet = timeSheet.OrderByDescending(x => x.Date).ToList();

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