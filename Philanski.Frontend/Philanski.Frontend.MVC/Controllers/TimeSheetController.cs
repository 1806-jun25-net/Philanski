﻿using System;
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

        public async Task<ActionResult> Create()
        {
            //api/timesheet/GetFullWeek?EmployeeId=id&&date={date}

            var uri ="api/timesheet/GetFullWeek?EmployeeId=1&&date=" + DateTime.Now.Date;
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