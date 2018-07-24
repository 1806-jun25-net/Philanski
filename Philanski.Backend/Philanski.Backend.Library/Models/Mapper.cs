﻿using System;
using System.Collections.Generic;
using System.Text;
using PBD = Philanski.Backend.Data;

namespace Philanski.Backend.Library.Models
{
    public static class Mapper
    {

        //notes on mapper
        //No object contains a whole other object
        //everything will be done by id
        //This may need to change later

        //Maps context employee to library employee
        public static Employee Map(PBD.Models.Employees employee) => new Employee
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            JobTitle = employee.JobTitle,
            WorksiteId = employee.WorksiteId,
            Salary = employee.Salary,
            HireDate = employee.HireDate,
            TerminationDate = employee.TerminationDate
        };

        //Maps libary employee to context employee
        public static PBD.Models.Employees Map(Employee employee) => new PBD.Models.Employees
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            JobTitle = employee.JobTitle,
            WorksiteId = employee.WorksiteId,
            Salary = employee.Salary,
            HireDate = employee.HireDate,
            TerminationDate = employee.TerminationDate
        };

        //Maps context department to library department
        public static Department Map(PBD.Models.Departments department) => new Department
        {
            Name = department.Name
        };

        //Maps library department to context department
        public static PBD.Models.Departments Map(Department department) => new PBD.Models.Departments
        {
            Name = department.Name
        };

        //Maps context manager to library manager
        public static Manager Map(PBD.Models.Managers manager) => new Manager
        {
            EmployeeId = manager.EmployeeId
        };

        //Maps library manager to context manager
        public static PBD.Models.Managers Map(Manager manager) => new PBD.Models.Managers
        {
            EmployeeId = manager.EmployeeId
        };

        //Maps context timesheet to library timesheet
        public static TimeSheet Map(PBD.Models.TimeSheets timesheet) => new TimeSheet
        {
            EmployeeId = timesheet.EmployeeId,
            Date = timesheet.Date,
            RegularHours = timesheet.RegularHours
        };

        //Maps library timesheet to context timesheet
        public static PBD.Models.TimeSheets Map(TimeSheet timesheet) => new PBD.Models.TimeSheets
        {
            EmployeeId = timesheet.EmployeeId,
            Date = timesheet.Date,
            RegularHours = timesheet.RegularHours
        };

        //Maps context TimeSheetApproval to library TimeSheetApproval
        public static TimeSheetApproval Map(PBD.Models.TimeSheetApprovals timesheetApproval) => new TimeSheetApproval
        {
            WeekStart = timesheetApproval.WeekStart,
            WeekEnd = timesheetApproval.WeekEnd,
            WeekTotalRegular = timesheetApproval.WeekTotalRegular,
            Status = timesheetApproval.Status,
            ApprovingManagerId = timesheetApproval.ApprovingManagerId,
            TimeSubmitted = timesheetApproval.TimeSubmitted,
            EmployeeId = timesheetApproval.EmployeeId
        };


        //Maps libary TimeSheetApproval to context TimeSheetApproval
        public static PBD.Models.TimeSheetApprovals Map(TimeSheetApproval timesheetApproval) => new PBD.Models.TimeSheetApprovals
        {
            WeekStart = timesheetApproval.WeekStart,
            WeekEnd = timesheetApproval.WeekEnd,
            WeekTotalRegular = timesheetApproval.WeekTotalRegular,
            Status = timesheetApproval.Status,
            ApprovingManagerId = timesheetApproval.ApprovingManagerId,
            TimeSubmitted = timesheetApproval.TimeSubmitted,
            EmployeeId = timesheetApproval.EmployeeId
        };
    }
}
