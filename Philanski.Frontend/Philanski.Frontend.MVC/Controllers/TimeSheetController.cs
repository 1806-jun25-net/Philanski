using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Philanski.Frontend.MVC.Models;

namespace Philanski.Frontend.MVC.Controllers
{
    public class TimeSheetController : AServiceController
    {
     

       // public HttpClient HttpClient { get; }
        // GET: Department

        public TimeSheetController(HttpClient httpClient) : base(httpClient)
        {
          
        }

        public async Task<ActionResult> Index()
        {
            if (TempData.Peek("username") == null)
            {
                return View("Forbidden");
            }
            var username = TempData.Peek("username");
            var uri = "api/employee/" + username + "/timesheet";
            var request = CreateRequestToService(HttpMethod.Get, uri);


            try
            {
                //get time sheets belonging to logged in employee
                var response = await HttpClient.SendAsync(request);

                if (response.StatusCode.Equals("Forbidden"))
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
                return View("Whoops");
            }
            catch(ArgumentNullException ex)
            {
                List<TimeSheets>[] ArrayOfListOfWeeks = new List<TimeSheets>[0];
                // logging
                return View(ArrayOfListOfWeeks);
            }

        }

        public async Task<ActionResult> Create(string weekstart)
        {
            if (TempData.Peek("username") == null)
            {
                return View("Forbidden");
            }
            var username = TempData.Peek("username");
            var uri = "api/employee/" + username + "/timesheet/" + weekstart;
            var request = CreateRequestToService(HttpMethod.Get, uri);

            try
            {
                var response = await HttpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    return View("Whoops");
                }

                string jsonString = await response.Content.ReadAsStringAsync();

                List<TimeSheets> timeSheet = JsonConvert.DeserializeObject<List<TimeSheets>>(jsonString);

                if (!timeSheet.Any())
                {
                    List<TimeSheets> newTimeSheet = new List<TimeSheets>();
                    DateTime now = DateTime.Now.Date;

                    while (now.DayOfWeek != DayOfWeek.Sunday)
                    {
                        now = now.AddDays(-1);
                    }

                    for (int i = 0; i < 7; i++)
                    {
                        TimeSheets newTS = new TimeSheets
                        {
                            Date = now,
                            RegularHours = 0,
                        };
                        newTimeSheet.Add(newTS);
                        now = now.AddDays(1);
                    }
                    newTimeSheet = newTimeSheet.OrderBy(x => x.Date).ToList();
                    return View(newTimeSheet);
                }

                //sort timeSheet by date monday-friday
                timeSheet = timeSheet.OrderBy(x => x.Date).ToList();

                return View(timeSheet);
            }
            catch (HttpRequestException ex)
            {
                // logging
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(List<TimeSheets> timeSheets)
        {
            if (TempData.Peek("username") == null)
            {
                return View("Forbidden");
            }
            var username = TempData.Peek("username");
            var uri = "api/employee/" + username + "/timesheet/" + timeSheets.ElementAt(0).Date.ToString("dd-MM-yyyy");
            var request = CreateRequestToService(HttpMethod.Get, uri);

            try
            {
                var response = await HttpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    return View("Whoops");
                }

                string jsonString = await response.Content.ReadAsStringAsync();

                List<TimeSheets> timeSheet = JsonConvert.DeserializeObject<List<TimeSheets>>(jsonString);

                if (!timeSheet.Any())
                {
                    string jsonPostString = JsonConvert.SerializeObject(timeSheets);
                    var PostUri = "api/employee/" + username + "/timesheet";

                    var postRequest = CreateRequestToService(HttpMethod.Post, PostUri);

                    postRequest.Content = new StringContent(jsonPostString, Encoding.UTF8, "application/json");

                    var postResponse = await HttpClient.SendAsync(postRequest);

                    if (!postResponse.IsSuccessStatusCode)
                    {
                        return View("Whoops");

                    }

                    return View(timeSheets);
                }
                else
                {
                    string jsonPutString = JsonConvert.SerializeObject(timeSheets);
                    var PutUri = "api/employee/" + username + "/timesheet/" + timeSheets.ElementAt(0).Date.ToString("dd-MM-yyyy");

                    var putRequest = CreateRequestToService(HttpMethod.Put, PutUri);

                    putRequest.Content = new StringContent(jsonPutString, Encoding.UTF8, "application/json");

                    var putResponse = await HttpClient.SendAsync(putRequest);

                    if (!putResponse.IsSuccessStatusCode)
                    {
                        return View("Whoops");
                    }
                    return View(timeSheets);
                }
            }
            catch
            {
                return View("Whoops");
            }

        }


        public async Task<ActionResult> PreviousTimesheet(string weekstart)
        {
            if (TempData.Peek("username") == null)
            {
                return View("Forbidden");
            }
            var username = TempData.Peek("username");
            var uri = "api/employee/" + username + "/timesheet/" + weekstart;
            var request = CreateRequestToService(HttpMethod.Get, uri);

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
                timeSheet = timeSheet.OrderBy(x => x.Date).ToList();

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