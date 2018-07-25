﻿using Philanski.Backend.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Philanski.Backend.Library.Models;

namespace Philanski.Backend.Library.Repositories
{
    public class Repository
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


        //IT WORKS

        public string testGetFirstEmployee()
        {
            string employeename = (from employee in _db.Employees
                                    where employee.Id.Equals(1)
                                   select employee.FirstName).SingleOrDefault();

            return employeename;

        }

        public int GetTimeSheetIdByDateAndEmpId(DateTime date, int employeeId)
        {
            TimeSheet timeSheet = Mapper.Map(_db.TimeSheets.First(i => i.EmployeeId == employeeId && i.Date == date));
            return timeSheet.Id;
        }


        //should work
        public Employee GetEmployeeByID(int ID)
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


        //need validation to make sure it works.
    /*    public Department GetDepartmentByID(int ID)
        {
            Departments deptfromdb = (from dept in _db.Departments
                                      where dept.Id.Equals(ID)
                                      select dept).SingleOrDefault();

            Department department = Mapper.Map(deptfromdb);

            return department;
        }*/

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

        public void Save()
        {
            _db.SaveChanges();
        }


    }
}
