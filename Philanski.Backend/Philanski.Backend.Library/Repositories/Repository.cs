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

    public class Repository: IRepository
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




        //Timesheet methods

        public async Task<int> GetTimeSheetIdByDateAndEmpId(DateTime date, int employeeId) //fix because mapper cant deal with null
        {
            var timeSheet = await _db.TimeSheets.FirstOrDefaultAsync(i => i.EmployeeId == employeeId && i.Date == date);
            if (timeSheet == null)
            {
                return 0;
            }
            return timeSheet.Id;
        }

        public async Task<TimeSheet> GetTimeSheetByID(int id)
        {

            var timesheet = await _db.TimeSheets.FirstOrDefaultAsync(x => x.Id == id);
            if (timesheet == null)
            {
                return null;
            }
            return Mapper.Map(timesheet);
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
            var TimeSheets =  _db.TimeSheets.Where(x => ((x.EmployeeId == employeeId) && (x.Date.CompareTo(DateStart) >= 0) && (x.Date.CompareTo(DateEnd) <= 0))).AsNoTracking();
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

        public async Task<TimeSheetApproval> GetTimeSheetApprovalById(int id)
        {
            var TimeSheetApprovals = await _db.TimeSheetApprovals.FirstOrDefaultAsync(x => x.Id == id);
            if (TimeSheetApprovals == null)
            {
                return null;
            }
            return Mapper.Map(TimeSheetApprovals);
        }

        public async Task<int> GetTimeSheetApprovalIdByDateSubmitted(DateTime submitted)
        {
            var TSA = await _db.TimeSheetApprovals.FirstOrDefaultAsync(i => i.TimeSubmitted == submitted);
            if (TSA == null)
            {
                return 0;
            }
            return TSA.Id;
        }

        public List<TimeSheetApproval> GetAllTimeSheetsFromEmployee(int EmployeeId)
        {
            var TimeSheetApprovals = _db.TimeSheetApprovals.Where(x => x.EmployeeId == EmployeeId).AsNoTracking();
            return Mapper.Map(TimeSheetApprovals);
        }

        public void CreateTimeSheetApproval(TimeSheetApproval TSA)
        {
            _db.Add(Mapper.Map(TSA));
        }

        //Employee Methods
        public async Task<Employee> GetEmployeeByID(int ID) //maybe change to find. NVM DONT USE FIND
        {
            var employee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == ID);
            if (employee == null)
            {
                return null;
            }
            return Mapper.Map(employee);
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

        public async Task<Manager> GetManagerById(int id)
        {
            var manager = await _db.Managers.FirstOrDefaultAsync(x => x.Id == id);
            if (manager == null)
            {
                return null;
            }
            return Mapper.Map(manager);
        }


        //Department methods
        public async Task<Department> GetDepartmentByID(int id)
        {
            var department = await _db.Departments.FirstOrDefaultAsync(x => x.Id == id);
            if (department == null)
            {
                return null;
            }
            return Mapper.Map(department);

        }

        public List<Department> GetAllDepartments()
        {
            var departments = _db.Departments.AsNoTracking();
            return Mapper.Map(departments);
        }

        public async Task<int> GetDepartmentIdByName(string name)
        {
            var department = await _db.Departments.FirstOrDefaultAsync(x => x.Name == name);
            if (department == null)
            {
                return 0;
            }
            return department.Id;
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
