using Philanski.Backend.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Philanski.Backend.Library.Repositories
{
   public interface IRepository
    {

        Task<int> GetTimeSheetIdByDateAndEmpId(DateTime date, int employeeId);
        Task<TimeSheet> GetTimeSheetByID(int id);
        List<TimeSheet> GetAllTimeSheets();
        List<TimeSheet> GetEmployeeTimeSheetWeekFromDate(DateTime date, int employeeId);
        void CreateTimeSheet(TimeSheet timesheet);
        List<TimeSheetApproval> GetAllTimeSheetApprovals();
        Task<TimeSheetApproval> GetTimeSheetApprovalById(int id);
        Task<int> GetTimeSheetApprovalIdByDateSubmitted(DateTime submitted);
        List<TimeSheetApproval> GetAllTimeSheetsFromEmployee(int EmployeeId);
        Task<Employee> GetEmployeeByID(int ID);
        List<Employee> GetAllEmployees();
        List<Manager> GetAllManagers();
        Manager GetManagerById(int id);
        Task<Department> GetDepartmentByID(int id);
        List<Department> GetAllDepartments();
        int GetDepartmentIdByName(string name);
        void CreateDepartment(Department department);
        void UpdateDepartment(Department department);
        void DeleteDepartment(int id);
        Task Save();





    }
}
