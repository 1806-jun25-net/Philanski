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


        //Method that takes a datetime and returns the sunday of that date's week. Will return with same time
        public DateTime GetPreviousSundayOfWeek(DateTime DateInWeek)
        {

            DateTime PreviousDate = DateInWeek;
            while (PreviousDate.DayOfWeek != DayOfWeek.Sunday)
            {
                PreviousDate = PreviousDate.AddDays(-1);
            }
            return PreviousDate;
        }

        //Method that takes a datetime and returns the saturday of that date's week. Will return with same time
        public DateTime GetNextSaturdayOfWeek(DateTime DateInWeek)
        {
            DateTime NextDate = DateInWeek;
            while (NextDate.DayOfWeek != DayOfWeek.Saturday)
            {
                NextDate = NextDate.AddDays(1);
            }
            return NextDate;
        }
    }



}
