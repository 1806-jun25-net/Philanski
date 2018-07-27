using Philanski.Backend.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Philanski.Backend.Library.Repositories
{
   public interface IRepository
    {

        int GetTimeSheetIdByDateAndEmpId(DateTime date, int employeeId);
        List<TimeSheet> GetAllTimeSheets();
        List<TimeSheet> GetEmployeeTimeSheetWeekFromDate(DateTime date, int employeeId);
        void CreateTimeSheet(TimeSheet timesheet);
        List<TimeSheetApproval> GetAllTimeSheetApprovals();
        TimeSheetApproval GetTimeSheetApprovalById(int id);
        List<TimeSheetApproval> GetAllTimeSheetsFromEmployee(int EmployeeId);
        Employee GetEmployeeByID(int ID);
        List<Employee> GetAllEmployees();
        List<Manager> GetAllManagers();
        Manager GetManagerById(int id);
        Department GetDepartmentByID(int id);
        List<Department> GetAllDepartments();
        int GetDepartmentIdByName(string name);
        void CreateDepartment(Department department);
        void UpdateDepartment(Department department);
        void DeleteDepartment(int id);
        Task Save();





    }
}
