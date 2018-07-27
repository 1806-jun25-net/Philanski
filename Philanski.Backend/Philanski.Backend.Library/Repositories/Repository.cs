using Philanski.Backend.DataContext.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Philanski.Backend.Library.Models;
using System.Threading.Tasks;

namespace Philanski.Backend.Library.Repositories
{

    public class Repository:IRepository
    {

        private readonly PhilanskiManagementSolutionsContext _db;

        /// <summary>
        /// Initializes a new philanski management solutions repo given a suitable Entity Framework DbContext.
        /// </summary>
        /// <param name="db">The DbContext</param>
        public Repository(PhilanskiManagementSolutionsContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }



        /* public string testGetFirstEmployee()
         {
             string employeename = (from employee in _db.Employees
                                     where employee.Id.Equals(1)
                                    select employee.FirstName).SingleOrDefault();

             return employeename;

         }*/


        //Timesheet methods

        public int GetTimeSheetIdByDateAndEmpId(DateTime date, int employeeId) //fix because mapper cant deal with null
        {
            TimeSheet timeSheet = Mapper.Map(_db.TimeSheets.FirstOrDefault(i => i.EmployeeId == employeeId && i.Date == date));
            if (timeSheet == null)
            {
                return 0;
            }
            return timeSheet.Id;
        }

        public TimeSheet GetTimeSheetByID(int id)
        {
            var timesheets = _db.TimeSheets.AsNoTracking();
            foreach (var timesheet in timesheets)
            {
                if(timesheet.Id.Equals(id))
                {
                    return Mapper.Map(timesheet);
                }
            }
            return null;
        }


        public List<TimeSheet> GetAllTimeSheets()
        {
            var TimeSheets = _db.TimeSheets.AsNoTracking();
            return Mapper.Map(TimeSheets);
        }

        public List<TimeSheet> GetEmployeeTimeSheetWeekFromDate(DateTime date, int employeeId)
        {
            //use date.date to get midnight
            var DateStart = TimeSheetApproval.GetPreviousSundayOfWeek(date.Date);
            var DateEnd = TimeSheetApproval.GetNextSaturdayOfWeek(date.Date);
            var TimeSheets = _db.TimeSheets.Where(x => ((x.EmployeeId == employeeId) && (x.Date.CompareTo(DateStart) >= 0) && (x.Date.CompareTo(DateEnd) <= 0))).AsNoTracking();
            if (TimeSheets == null)
            {
                return null;
            }
            return Mapper.Map(TimeSheets);
        }

        public void CreateTimeSheet(TimeSheet timesheet)
        {
            _db.Add(Mapper.Map(timesheet));
        }

        //TimeSheet Approval Methods


        public List<TimeSheetApproval> GetAllTimeSheetApprovals()
        {
            var TimeSheetApprovals = _db.TimeSheetApprovals.AsNoTracking();
            return Mapper.Map(TimeSheetApprovals);
        }

        public TimeSheetApproval GetTimeSheetApprovalById(int id)
        {
            var TimeSheetApprovals = _db.TimeSheetApprovals;
            foreach (var TSA in TimeSheetApprovals)
            {
                if (TSA.Id.Equals(id))
                {
                    return Mapper.Map(TSA);
                }
            }
            return null;
        }

        public int GetTimeSheetApprovalIdByDateSubmitted(DateTime submitted)
        {
            var TSA = Mapper.Map(_db.TimeSheetApprovals.FirstOrDefault(i => i.TimeSubmitted == submitted));
            if (TSA == null)
            {
                return 0;
            }
            return TSA.Id;
        }

        public List<TimeSheetApproval> GetAllTimeSheetsFromEmployee(int EmployeeId)
        {
            var TimeSheetApprovals = _db.TimeSheetApprovals.Where(x => x.EmployeeId == EmployeeId);
            if (TimeSheetApprovals == null)
            {
                return null;
            }
            return Mapper.Map(TimeSheetApprovals);
        }

        public void CreateTimeSheetApproval(TimeSheetApproval TSA)
        {
            _db.Add(Mapper.Map(TSA));
        }

        //Employee Methods
        public Employee GetEmployeeByID(int ID) //maybe change to find. NVM DONT USE FIND
        {
            var employees = _db.Employees;
            foreach (var emp in employees)
            {
                if (emp.Id.Equals(ID))
                {
                    return Mapper.Map(emp);
                }
            }
            return null;
        }

        public List<Employee> GetAllEmployees()
        {
            var employees = _db.Employees.AsNoTracking();
            return Mapper.Map(employees);
        }


        //Manager Methods
        public List<Manager> GetAllManagers()
        {
            var managers = _db.Managers.AsNoTracking();
            return Mapper.Map(managers);
        }

        public Manager GetManagerById(int id)
        {
            var managers = _db.Managers;
            foreach (var manager in managers)
            {
                if (manager.Id.Equals(id))
                {
                    return Mapper.Map(manager);
                }
            }
            return null;

        }


        //Department methods
        public Department GetDepartmentByID(int id)
        {
            var departments = _db.Departments;
            foreach (var dept in departments)
            {
                if (dept.Id.Equals(id))
                {
                    return Mapper.Map(dept);
                }
            }
            return null;
        }

        public List<Department> GetAllDepartments()
        {
            var departments = _db.Departments.AsNoTracking();
            return Mapper.Map(departments);
        }

        public int GetDepartmentIdByName(string name)
        {
            var departments = _db.Departments;
            foreach (var dept in departments)
            {
                if (dept.Name.Equals(name))
                {
                    return dept.Id;
                }
            }
            return 0;
        }

        public void CreateDepartment(Department department)
        {
            _db.Add(Mapper.Map(department));
           
        } 

        public void UpdateDepartment(Department department)
        {
            //mapper doesnt include library -> context id keeping. need Id for update
            //also dont want names that are already in database, so need to check that too
            //potential fix later
            var dbDept = Mapper.Map(department);
            dbDept.Id = department.Id;
            _db.Entry(_db.Departments.Find(department.Id)).CurrentValues.SetValues(dbDept);
        }

        public void DeleteDepartment(int id)

        {
            _db.Remove(_db.Departments.Find(id));
            
        }

        public Task Save()
        {
           return _db.SaveChangesAsync();
        }


    }
}
