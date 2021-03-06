using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Philanski.Frontend.MVC.Models
{
    public class TimeSheetApprovals
    {
        public int Id { get; set; }
        [Display(Name = "Week Start")]
        public DateTime WeekStart { get; set; }
        [Display(Name = "Week End")]
        public DateTime WeekEnd { get; set; }
        public decimal WeekTotalRegular { get; set; }
        public string Status { get; set; }
        [Display(Name = "Approving Manager Id")]
        public int? ApprovingManagerId { get; set; }
        [Display(Name = "Time Submitted")]
        public DateTime TimeSubmitted { get; set; }
        public int EmployeeId { get; set; }

        //public Repository Repo { get; }


        ////Method that takes a datetime and returns the sunday of that date's week. Will return with same time
        //public DateTime GetPreviousSundayOfWeek(DateTime DateInWeek)
        //{

        //    DateTime PreviousDate = DateInWeek;
        //    while (PreviousDate.DayOfWeek != DayOfWeek.Sunday)
        //    {
        //        PreviousDate = PreviousDate.AddDays(-1);
        //    }
        //    return PreviousDate;
        //}

        ////Method that takes a datetime and returns the saturday of that date's week. Will return with same time
        //public DateTime GetNextSaturdayOfWeek(DateTime DateInWeek)
        //{
        //    DateTime NextDate = DateInWeek;
        //    while (NextDate.DayOfWeek != DayOfWeek.Saturday)
        //    {
        //        NextDate = NextDate.AddDays(1);
        //    }
        //    return NextDate;
        //}

        ////Method that takes week start and employeeID
        ////Returns an array of ID's that represent all the time punches for a specific week by that employee
        //public int[] GetWeekTimePunches(int employeeId, DateTime weekStart)
        //{
        //    DateTime[] weekArray = new DateTime[7];

        //    //creates an array with all the days in the timesheet
        //    for (int i = 0; i < 6; i++)
        //    {
        //        weekArray[i] = weekStart.AddDays(i);
        //    }

        //    int[] timePunchIDs = new int[7];

        //    //creates an array of IDs of the time punches by the employee for the given week
        //    for (int i = 0; i < weekArray.Length; i++)
        //    {
        //        timePunchIDs[i] = Repo.GetTimeSheetIdByDateAndEmpId(weekArray[i], employeeId);
        //    }

        //    return timePunchIDs;

        //}



    }



}
