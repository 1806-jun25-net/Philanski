using Philanski.Backend.Library.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Philanski.Backend.Library.Models
{
    public class TimeSheetApproval
    {
        public int Id { get; set; }
        public DateTime WeekStart { get; set; }
        public DateTime WeekEnd { get; set; }
        public decimal WeekTotalRegular { get; set; }
        public string Status { get; set; }
        public int? ApprovingManagerId { get; set; }
        public DateTime TimeSubmitted { get; set; }
        public int EmployeeId { get; set; }

      //  public Repository Repo { get; }

        
        //Method that takes a datetime and returns the sunday of that date's week. Will return with same time
        public static DateTime GetPreviousSundayOfWeek(DateTime DateInWeek)
        { 

            DateTime PreviousDate = DateInWeek;
            while (PreviousDate.DayOfWeek != DayOfWeek.Sunday)
            {
                PreviousDate = PreviousDate.AddDays(-1);
            }
            return PreviousDate;
        }

        //Method that takes a datetime and returns the saturday of that date's week. Will return with same time
        public static DateTime GetNextSaturdayOfWeek(DateTime DateInWeek)
        {
            DateTime NextDate = DateInWeek;
            while (NextDate.DayOfWeek != DayOfWeek.Saturday)
            {
                NextDate = NextDate.AddDays(1);
            }
            return NextDate;
        }

        //Method that takes week start and employeeID
        //Returns an array of ID's that represent all the time punches for a specific week by that employee

        //Dont call repo in Library classes.
        //We can do all this in the repo

     /*   public int[] GetWeekTimePunches(int employeeId, DateTime weekStart)
        {
            DateTime[] weekArray = new DateTime[7];

            //creates an array with all the days in the timesheet
            for (int i = 0; i < 6; i++)
            {
                weekArray[i] = weekStart.AddDays(i);
            }

            int[] timePunchIDs = new int[7];

            //creates an array of IDs of the time punches by the employee for the given week
            for(int i = 0; i < weekArray.Length; i++)
            {
                timePunchIDs[i] = Repository.GetTimeSheetIdByDateAndEmpId(weekArray[i], employeeId);
            }

            return timePunchIDs;

        }*/



    }



}
