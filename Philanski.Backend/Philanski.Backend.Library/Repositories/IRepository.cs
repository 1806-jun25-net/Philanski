using Philanski.Backend.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Philanski.Backend.Library.Repositories
{
   public interface IRepository
    {
        //Split methods up by class
        //Time Sheets
        Task<int> GetTimeSheetIdByDateAndEmpId(DateTime date, int employeeId);
        Task<TimeSheet> GetTimeSheetByID(int id);
        List<TimeSheet> GetAllTimeSheets();
        List<TimeSheet> GetTimeSheetsByEmployeeId(int employeeId);
        List<TimeSheet> GetEmployeeTimeSheetWeekFromDate(DateTime date, int employeeId);
        void CreateTimeSheet(TimeSheet timesheet);
        void UpdateTimeSheet(TimeSheet timesheet);


        //Time Sheet Approvals
        List<TimeSheetApproval> GetAllTimeSheetApprovals();
        Task<TimeSheetApproval> GetTimeSheetApprovalById(int id);
        Task<int> GetTimeSheetApprovalIdByDateSubmitted(DateTime submitted);
        List<TimeSheetApproval> GetAllTimeSheetsFromEmployee(int EmployeeId);
        void CreateTimeSheetApproval(TimeSheetApproval TSA);
        Task<List<TimeSheetApproval>> GetAllTSAsThatCanBeApprovedByManager(int id);
        Task<TimeSheetApproval> GetTimeSheetApprovalByEmployeeIdAndWeekStart(int EmployeeId, DateTime WeekStart);
        void UpdateTimeSheetApproval(TimeSheetApproval TSA);


        //Employees
        Task<int> GetEmployeeIDByEmail(string email);
        Task<Employee> GetEmployeeByID(int ID);
        List<Employee> GetAllEmployees();
        Task<Employee> GetEmployeeByEmail(string email);
        void CreateEmployee(Employee employee);
        //Managers
        List<Manager> GetAllManagers();
        Task<Manager> GetManagerById(int id);
        Task<int> GetManagerIdByEmployeeId(int id);

        //EmployeeDepartment methods
        Task<List<int>> GetAllDepartmentIdsByEmployee(int id);

        //ManagerDepartment methods
        Task<List<int>> GetAllDepartmentIdsByManagerId(int id);

        //Departments
        Task<Department> GetDepartmentByID(int id);
        List<Department> GetAllDepartments();
        Task<int> GetDepartmentIdByName(string name);
        void CreateDepartment(Department department);
        void UpdateDepartment(Department department);
        void DeleteDepartment(int id);

        //General
        Task Save();





    }
}
