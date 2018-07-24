using System;
using System.Collections.Generic;
using System.Linq;
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
            Id = employee.Id,
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
            Id = department.Id,
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
            Id = manager.Id,
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
            Id = timesheet.Id,
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
            Id = timesheetApproval.Id,
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

        //Map an IEnumerable of libary employees to context employees. Vice versa
        public static IEnumerable<Employee> Map(IEnumerable<PBD.Models.Employees> employees) => employees.Select(Map);
        public static IEnumerable<PBD.Models.Employees> Map(IEnumerable<Employee> employees) => employees.Select(Map);

        //Map an IEnumerable of context departments to library departments. Vice versa
        public static IEnumerable<Department> Map(IEnumerable<PBD.Models.Departments> departments) => departments.Select(Map);
        public static IEnumerable<PBD.Models.Departments> Map(IEnumerable<Department> departments) => departments.Select(Map);

        //Map an IEnumerable of library Managers to context Managers. Vice versa.
        public static IEnumerable<Manager> Map(IEnumerable<PBD.Models.Managers> managers) => managers.Select(Map);
        public static IEnumerable<PBD.Models.Managers> Map(IEnumerable<Manager> managers) => managers.Select(Map);

        //Map an IEnumerable of library TimeSheets and context TimeSheets. Vice versa.
        public static IEnumerable<TimeSheet> Map(IEnumerable<PBD.Models.TimeSheets> timesheets) => timesheets.Select(Map);
        public static IEnumerable<PBD.Models.TimeSheets> Map(IEnumerable<TimeSheet> timesheets) => timesheets.Select(Map);

        //Map an IEnumerable of library Timesheet Approvals to Conext TimeSheet Approvals. ViceVersa.
        public static IEnumerable<TimeSheetApproval> Map(IEnumerable<PBD.Models.TimeSheetApprovals> timesheetApprovals) => timesheetApprovals.Select(Map);
        public static IEnumerable<PBD.Models.TimeSheetApprovals> Map(IEnumerable<TimeSheetApproval> timesheetApprovals) => timesheetApprovals.Select(Map);
    }
}
