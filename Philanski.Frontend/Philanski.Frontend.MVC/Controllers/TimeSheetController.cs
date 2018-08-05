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
            //need to add code that disables submit button if TSA exists
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

                //make a call to get gettimesheetapprovalbyweekstart

               
                var uri2 = "api/employee/" + username + "/timesheetapproval/" + weekstart;
                var request2 = CreateRequestToService(HttpMethod.Get, uri2);

                try
                {
                    var response2 = await HttpClient.SendAsync(request2);

                    string jsonString2 = await response2.Content.ReadAsStringAsync();

                    TimeSheetApprovals timeSheetApproval = JsonConvert.DeserializeObject<TimeSheetApprovals>(jsonString2);

                    //need if TSA exists here basically

                    if(timeSheetApproval != null)
                    {
                        return View("PreviousTimesheet", timeSheet);

                    }


                }

                  catch (HttpRequestException ex)
                 {
                return View("Whoops");
                  }
            catch (ArgumentNullException ex)
                 {
                List<TimeSheets>[] ArrayOfListOfWeeks = new List<TimeSheets>[0];
                // logging
                return View(ArrayOfListOfWeeks);
                 }

           



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
        //right now the user can create a timesheet based on the current week. it will handle whether they have created a timesheet before
        //or not. When they submit it will add a TSA to the database or tell them that already submitted their timesheet.
        //Issues: The fields are always open, so even after submitting the TSA they can change their hours in the DB.
        //These changes wont be reflected in the timesheetapproval
        public async Task<ActionResult> Create(List<TimeSheets> timeSheets, string submit)
        {

            if (TempData.Peek("username") == null)
            {
                return View("Forbidden");
            }
            decimal totalhours = 0;
            foreach (var timesheet in timeSheets)
            {
                totalhours = timesheet.RegularHours + totalhours;
            }
            if (totalhours > 60.00m)
            {
                ModelState.AddModelError(string.Empty, "Can't submit or save a time sheet over 60 hours");
                return View(timeSheets);
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
                    if (submit.Equals("Submit"))
                    {
                        //create TSA 
                        var TSA = new TimeSheetApprovals
                        {
                            WeekStart = timeSheets.ElementAt(0).Date,
                            WeekEnd = timeSheets.ElementAt(6).Date,
                            Status = "0",
                            TimeSubmitted = DateTime.Now
                        };
                        decimal HoursWorked = 0;
                        foreach (var item in timeSheets)
                        {
                            HoursWorked = HoursWorked + item.RegularHours;
                        }
                        TSA.WeekTotalRegular = HoursWorked;
                        var PostTSAUri = "api/employee/" + username + "/timesheetapproval/";
                        var PostTSAJsonString = JsonConvert.SerializeObject(TSA);
                        var postTSARequest = CreateRequestToService(HttpMethod.Post, PostTSAUri);
                        postTSARequest.Content = new StringContent(PostTSAJsonString, Encoding.UTF8, "application/json");
                        var postTSAResponse = await HttpClient.SendAsync(postTSARequest);
                        //add employee id on back end
                        if (postTSAResponse.IsSuccessStatusCode.Equals("Conflict"))
                        {
                            return View("TSAAlreadySubmitted");
                        }
                        if (!postTSAResponse.IsSuccessStatusCode)
                        {
                            return View("Whoops");
                        }

                        return View("TSASubmitted");
                    }
                    else
                    {
                        return View(timeSheets);
                    }
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
                    if (submit.Equals("Submit"))
                    {
                        //return View("Forbidden");
                        //create TSA
                        var TSA = new TimeSheetApprovals
                        {
                            WeekStart = timeSheets.ElementAt(0).Date,
                            WeekEnd = timeSheets.ElementAt(6).Date,
                            Status = "0",
                            TimeSubmitted = DateTime.Now
                        };
                        decimal HoursWorked = 0;
                        foreach(var item in timeSheets)
                        {
                            HoursWorked = HoursWorked + item.RegularHours;
                        }
                        TSA.WeekTotalRegular = HoursWorked;
                        var PostTSAUri = "api/employee/" + username + "/timesheetapproval/";
                        var PostTSAJsonString = JsonConvert.SerializeObject(TSA);
                        var postTSARequest = CreateRequestToService(HttpMethod.Post, PostTSAUri);
                        postTSARequest.Content = new StringContent(PostTSAJsonString, Encoding.UTF8, "application/json");
                        var postTSAResponse = await HttpClient.SendAsync(postTSARequest);
                        //add employee id on back end
                        if (postTSAResponse.IsSuccessStatusCode.Equals(409))
                        {
                            return View("TSAAlreadySubmitted");
                        }
                        if (!postTSAResponse.IsSuccessStatusCode)
                        {
                            return View("Whoops");
                        }

                        return View("TSASubmitted");


                    }
                    else
                    {   
                        return View(timeSheets);
                    }
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