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

        public List<TimeSheet> GetTimeSheetsByEmployeeId(int employeeId)
        {
            var TimeSheets = _db.TimeSheets.Where(x => x.EmployeeId == employeeId).AsNoTracking();
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

        public void UpdateTimeSheet(TimeSheet timesheet)
        {
            //mapper doesnt include library -> context id keeping. need Id for update
            //also dont want names that are already in database, so need to check that too
            //potential fix later
            var dbTimeSheet = Mapper.Map(timesheet);
            dbTimeSheet.Id = timesheet.Id;
            _db.TimeSheets.Attach(dbTimeSheet);
            //_db.Entry(_db.Departments.Find(timesheet.Id)).CurrentValues.SetValues(dbTimeSheet);
            _db.Entry(dbTimeSheet).Property(x => x.RegularHours).IsModified = true;
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

        public async Task<TimeSheetApproval> GetTimeSheetApprovalByEmployeeIdAndWeekStart(int EmployeeId, DateTime WeekStart)
        {
            var TSA = await _db.TimeSheetApprovals.FirstOrDefaultAsync(i => (i.EmployeeId == EmployeeId) && (i.WeekStart == WeekStart));
            if (TSA == null)
            {
                return null;
            }
            return Mapper.Map(TSA);
        }

        public void CreateTimeSheetApproval(TimeSheetApproval TSA)
        {
            _db.Add(Mapper.Map(TSA));
        }

        //get approvals by manager id
        //logic 
        //What do I have: manager id->all departments they are part of. 
        //employee ids on TSAs. check their department and and if part of managers. add to list
        public async Task<List<TimeSheetApproval>> GetAllTSAsThatCanBeApprovedByManager(int id)
        {
            var TSAs = _db.TimeSheetApprovals.AsNoTracking();
            var ManagerDeptIds = await GetAllDepartmentIdsByManagerId(id);
            List<TimeSheetApprovals> TSAForManager = new List<TimeSheetApprovals>();
            foreach (var TSA in TSAs)
            {
                //gather employee departments, compare their departments to manager depts. if a match add to list of tsas.
                var EmployeeDeptIds = await GetAllDepartmentIdsByEmployee(TSA.EmployeeId);
                if (ManagerDeptIds.Intersect(EmployeeDeptIds).Any())
                {
                    TSAForManager.Add(TSA);
                }
            }
            return Mapper.Map(TSAForManager);
        }

        //Employee-Department methods
        public async Task<List<int>> GetAllDepartmentIdsByEmployee(int id)
        {
            var departments = await _db.EmployeeDepartments.Where(x => x.EmployeeId == id).ToListAsync();
            List<int> deptIds = new List<int>();
            foreach (var dept in departments)
            {
                deptIds.Add(dept.DepartmentId);
            }
            return deptIds;
        }

        //Manager-Department methods

        public async Task<List<int>> GetAllDepartmentIdsByManagerId(int id)
        {
            var departments = await _db.ManagerDepartments.Where(x => x.ManagerId == id).ToListAsync();
            List<int> deptIds = new List<int>();
            foreach (var dept in departments)
            {
                deptIds.Add(dept.DepartmentId);
            }
            return deptIds;
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

        public async Task<Employee> GetEmployeeByEmail(string email)
        {
           var employee = await _db.Employees.FirstOrDefaultAsync(x => x.Email == email);
            if(employee == null)
            {
                return null;
            }
            return Mapper.Map(employee);

        }

        public async Task<int> GetEmployeeIDByEmail(string email)
        {
            var employee = await _db.Employees.FirstOrDefaultAsync(x => x.Email == email);
            if(employee == null)
            {
                return 0;
            }

            return employee.Id;


        }

        public List<Employee> GetAllEmployees()
        {
            var employees = _db.Employees.AsNoTracking();
            return Mapper.Map(employees);
        }


        public void CreateEmployee(Employee employee)
        {
            _db.Add(Mapper.Map(employee));

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
