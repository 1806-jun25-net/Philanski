using System;
using System.Collections.Generic;
using System.Text;

namespace Philanski.Backend.Library.Models
{
    public class TimeSheetApproval
    {
        public DateTime WeekStart;
        public DateTime WeekEnd;


        public DateTime GetPreviousSundayOfWeek(DateTime DateInWeek)
        {

            DateTime PreviousDate = DateInWeek;
            while (PreviousDate.DayOfWeek != DayOfWeek.Sunday)
            {
                PreviousDate = PreviousDate.AddDays(-1);
            }
            return PreviousDate;
        }

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
