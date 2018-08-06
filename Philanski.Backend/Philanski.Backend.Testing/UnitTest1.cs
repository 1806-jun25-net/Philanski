using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using Philanski.Backend.DataContext.Models;
using Philanski.Backend.Library.Models;
using Philanski.Backend.Library.Repositories;
using Philanski.Backend.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using System.Threading.Tasks;

namespace Philanski.Backend.Testing
{
    public class UnitTest1
    {



        //ensuredatabasecreated still needs to be used somewhere. 
        //  DbContextOptionsBuilder<PhilanskiManagementSolutionsContext> optionsBuilder = new DbContextOptionsBuilder<PhilanskiManagementSolutionsContext>().UseInMemoryDatabase();
        //public static DbContextOptionsBuilder UseInMemoryDatabase(this DbContextOptionsBuilder optionsBuilder, Action<InMemoryDbContextOptionsBuilder> inMemoryOptionsAction = null);
        //    optionsBuilder
        //.UseSqlServer(connectionString, providerOptions=>providerOptions.CommandTimeout(60))
        //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        //  PhilanskiManagementSolutionsContext db = new PhilanskiManagementSolutionsContext();





        [Theory]
        [InlineData("07/23/2018 14:50:50.42", "07/22/2018 14:50:50.42")]
        [InlineData("07/22/2018 14:50:50.42", "07/22/2018 14:50:50.42")]
        public void GetPreviousSundayTest(string test, string actual)
        {
            // var TimeSheetApp = new TimeSheetApproval();
            var testDate = Convert.ToDateTime(test);
            var result = TimeSheetApproval.GetPreviousSundayOfWeek(testDate);
            Assert.Equal(Convert.ToDateTime(actual), result);

        }

        [Theory]
        [InlineData("07/23/2018 14:50:50.42", "07/28/2018 14:50:50.42")]
        [InlineData("07/28/2018 14:50:50.42", "07/28/2018 14:50:50.42")]
        public void GetNextSaturdayTest(string test, string actual)
        {
            //      var TimeSheetApp = new TimeSheetApproval();
            var testDate = Convert.ToDateTime(test);
            var result = TimeSheetApproval.GetNextSaturdayOfWeek(testDate);
            Assert.Equal(Convert.ToDateTime(actual), result);

        }


        //TESTING DEPARTMENT API CONTROLLER
        [Fact]
        public void GetAllShouldReturnAllDepartments()
        {
            // arrange
            var deptList = new List<Department>
            {
                new Department { Id = 1, Name = "Blacksmithing" },
                new Department { Id = 2, Name = "Jewelcrafting" }
            };
            var mockRepo = new Mock<IRepository>();
            mockRepo.Setup(x => x.GetAllDepartments()).Returns(deptList);
            DepartmentController controller = new DepartmentController(mockRepo.Object);

            // act
            var actionResult = controller.GetAll();

            // assert
            // assert that actionResult.Value is not null

            Assert.NotNull(actionResult.Value);

            // instead of view.model, actionResult.Value


            //var view = Assert.IsType<ViewResult>(actionResult);
            var model = Assert.IsAssignableFrom<List<Department>>(actionResult.Value).ToList();
            Assert.Equal(deptList.Count, model.Count);
            for (int i = 0; i < model.Count; i++)
            {
                Assert.Equal(deptList[i].Id, model[i].Id);
                Assert.Equal(deptList[i].Name, model[i].Name);
            }
        }

        //TESTING employee API CONTROLLER
        [Fact]
        public void TestingEmpController()
        {

        //      public int Id { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Email { get; set; }
        //public string JobTitle { get; set; }
        //public int WorksiteId { get; set; }
        //public decimal Salary { get; set; }
        //public DateTime HireDate { get; set; }
        //public DateTime? TerminationDate { get; set; }
        // arrange
        var empList = new List<Employee>
            {
                new Employee { Id = 1, FirstName = "Jeff" , LastName = "Jeff", Email = "jeffjeff@gmail.com", JobTitle = "Tester", WorksiteId = 1, Salary = 20000.00m, HireDate= DateTime.Now, TerminationDate = null},
                new Employee { Id = 1, FirstName = "Jeff" , LastName = "Jeff", Email = "jeffjeff@gmail.com", JobTitle = "Tester", WorksiteId = 1, Salary = 20000.00m, HireDate= DateTime.Now, TerminationDate = null}
            };
            var mockRepo = new Mock<IRepository>();
            mockRepo.Setup(x => x.GetAllEmployees()).Returns(empList);
            EmployeeController controller = new EmployeeController(mockRepo.Object);

            // act
            var actionResult = controller.GetAll();

            // assert
            // assert that actionResult.Value is not null

            Assert.NotNull(actionResult.Value);

            // instead of view.model, actionResult.Value


            //var view = Assert.IsType<ViewResult>(actionResult);
            var model = Assert.IsAssignableFrom<List<Employee>>(actionResult.Value).ToList();
            Assert.Equal(empList.Count, model.Count);
            for (int i = 0; i < model.Count; i++)
            {
                Assert.Equal(empList[i].Id, model[i].Id);
                Assert.Equal(empList[i].FirstName, model[i].FirstName);
            }
        }




        //testing all department repo methods
        [Fact]
        public async Task DepartmentRepoMethodsTest()
        {

            var options = new DbContextOptionsBuilder<PhilanskiManagementSolutionsContext>()
                 .UseInMemoryDatabase(databaseName: "TestDB2")
                 .Options;


            Department testdept1 = new Department();
            testdept1.Id = 1;
            testdept1.Name = "Blacksmithing";

            Department testdept2 = new Department();
            testdept2.Id = 2;
            testdept2.Name = "Jewelcrafting";


            // Run the test against one instance of the context
            using (var context = new PhilanskiManagementSolutionsContext(options))
            {
                var repo = new Repository(context);
                repo.CreateDepartment(testdept1);
                await repo.Save();
                repo.CreateDepartment(testdept2);
                await repo.Save();
                
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new PhilanskiManagementSolutionsContext(options))
            {
                var repo = new Repository(context);

                List<Department> testdeptlist = new List<Department>();

                //testing get all depts
                testdeptlist = repo.GetAllDepartments();
                Assert.Equal(2, testdeptlist.Count());


                //TESTinG getdeptby id
                Department testgetbyID = await repo.GetDepartmentByID(2);

                Assert.Equal("Jewelcrafting", testgetbyID.Name);


                //null path of getdeptby id tested
                Department testgetbyIDnull = await repo.GetDepartmentByID(3);

                Assert.Null(testgetbyIDnull);





                //testing get apt id by name

                int aptidtest = await repo.GetDepartmentIdByName("Jewelcrafting");

                Assert.Equal(2, aptidtest);


                //testing delete apartment 

                repo.DeleteDepartment(2);
                await repo.Save();

                List<Department> testdeptlist2 = repo.GetAllDepartments();

                //should only be one left
                Assert.Single(testdeptlist2);


                //testing update apartment

                Department updatetestdept = new Department();
                updatetestdept.Id = 1;
                updatetestdept.Name = "New Department";

                repo.UpdateDepartment(updatetestdept);

                Department checkingitwasupdated = new Department();

                checkingitwasupdated = await repo.GetDepartmentByID(1);

                Assert.Equal("New Department", checkingitwasupdated.Name);

                context.Database.EnsureDeleted();
            }

          

        }


        //testing all timesheetmethods

        [Fact]
        public async Task TestingTimesheetRepoMethods()
        {

            var options = new DbContextOptionsBuilder<PhilanskiManagementSolutionsContext>()
                 .UseInMemoryDatabase(databaseName: "TestDB3")
                 .Options;


            TimeSheet timesheet1 = new TimeSheet();
            timesheet1.Date = DateTime.Now;
            timesheet1.EmployeeId = 1;
            timesheet1.Id = 1;
            timesheet1.RegularHours = 40.00m;


            TimeSheet timesheet2 = new TimeSheet();
            timesheet2.Date = DateTime.Now;
            timesheet2.EmployeeId = 2;
            timesheet2.Id = 2;
            timesheet2.RegularHours = 30.00m;



            TimeSheet updatedtimesheet2 = new TimeSheet();
            updatedtimesheet2.Date = timesheet2.Date;
            updatedtimesheet2.EmployeeId = 2;
            updatedtimesheet2.Id = 2;
            updatedtimesheet2.RegularHours = 35.00m;

            TimeSheetApproval TSA = new TimeSheetApproval();
            TSA.WeekEnd = DateTime.Parse("07/29/2018 14:50:50.42");
            TSA.WeekStart = DateTime.Parse("07/22/2018 14:50:50.42");
            TSA.WeekTotalRegular = 56.00m;
            TSA.Status = "0";
            TSA.TimeSubmitted = DateTime.Now;
            TSA.EmployeeId = 1;
      


            // Run the test against one instance of the context
            using (var context = new PhilanskiManagementSolutionsContext(options))
            {
                var repo = new Repository(context);
                repo.CreateTimeSheet(timesheet1);
                await repo.Save();
                repo.CreateTimeSheet(timesheet2);
                await repo.Save();
                repo.CreateTimeSheetApproval(TSA);
                await repo.Save();



            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new PhilanskiManagementSolutionsContext(options))
            {
                var repo = new Repository(context);

                List<TimeSheet> testtimesheetlist = new List<TimeSheet>();

                //testing get all timesheets
                testtimesheetlist = repo.GetAllTimeSheets();
                Assert.Equal(2, testtimesheetlist.Count());


                //TESTinG Task<int> GetTimeSheetIdByDateAndEmpId(DateTime date, int employeeId);
                int testtimesheetid = await repo.GetTimeSheetIdByDateAndEmpId(timesheet1.Date, 1);

                Assert.Equal(1, testtimesheetid);


                //testing  Task<TimeSheet> GetTimeSheetByID(int id);

                TimeSheet timesheetfromgetbyid = await repo.GetTimeSheetByID(1);
                Assert.Equal(timesheetfromgetbyid.Date, timesheet1.Date);


                // testing  List<TimeSheet> GetTimeSheetsByEmployeeId(int employeeId);

                List<TimeSheet> emp2timesheets = repo.GetTimeSheetsByEmployeeId(2);
                Assert.Single(emp2timesheets);

                //testing List<TimeSheet> GetEmployeeTimeSheetWeekFromDate(DateTime date, int employeeId);

                List<TimeSheet> timesheetsbydate = repo.GetEmployeeTimeSheetWeekFromDate(timesheet2.Date, 2);
                Assert.Single(timesheetsbydate);

                //testing void UpdateTimeSheet(TimeSheet timesheet);

                repo.UpdateTimeSheet(updatedtimesheet2);

                TimeSheet testingifitupdated = await repo.GetTimeSheetByID(2);

                Assert.Equal(35.00m, testingifitupdated.RegularHours);

                var TSAs = repo.GetAllTimeSheetApprovals();
                Assert.Single(TSAs);

                TSAs = repo.GetAllTimeSheetsFromEmployee(TSA.EmployeeId);
                Assert.Single(TSAs);

                var TSAtest = await repo.GetTimeSheetApprovalById(1);
                Assert.Equal(1, TSAtest.EmployeeId);


                context.Database.EnsureDeleted();
            }



        }

        //testing getalldepartmnetsidbymanager repo method
        public class UnitTest2
        {
            [Fact]
            public async Task GetAllDepartmentIdsByManagerIdTest()
            {

                var options = new DbContextOptionsBuilder<PhilanskiManagementSolutionsContext>()
                    .UseInMemoryDatabase(databaseName: "TestDB")
                    .Options;



                Department testdept1 = new Department();
                testdept1.Id = 1;
                testdept1.Name = "Blacksmithing";

                Department testdept2 = new Department();
                testdept2.Id = 2;
                testdept2.Name = "Jewelcrafting";


                Employee testregremp1 = new Employee();
                testregremp1.Email = "bobman76@gmail.com";
                testregremp1.FirstName = "Bob";
                testregremp1.HireDate = DateTime.Now;
                testregremp1.Id = 1;
                testregremp1.JobTitle = "The Terminator";
                testregremp1.LastName = "Man";
                testregremp1.Salary = 55000.00m;
                testregremp1.WorksiteId = 1;

                Employee testregremp2 = new Employee();
                testregremp2.Email = "timmy22@gmail.com";
                testregremp2.FirstName = "Timmy";
                testregremp2.HireDate = DateTime.Now;
                testregremp2.Id = 2;
                testregremp2.JobTitle = "The Tim";
                testregremp2.LastName = "Marely";
                testregremp2.Salary = 99000.00m;
                testregremp2.WorksiteId = 2;

                Employee testmanageremp = new Employee();
                testmanageremp.Email = "iamamanager@gmail.com";
                testmanageremp.FirstName = "The";
                testmanageremp.HireDate = DateTime.Now;
                testmanageremp.Id = 3;
                testmanageremp.JobTitle = "Bossman";
                testmanageremp.LastName = "Boss";
                testmanageremp.Salary = 99000.00m;
                testmanageremp.WorksiteId = 2;





                // Run the test against one instance of the context
                using (var context = new PhilanskiManagementSolutionsContext(options))
                {
                    var repo = new Repository(context);

                    repo.CreateEmployee(testregremp1);
                    await repo.Save();
                    repo.CreateEmployee(testregremp2);
                    await repo.Save();
                    repo.CreateEmployee(testmanageremp);
                    await repo.Save();
                    repo.CreateDepartment(testdept1);
                    await repo.Save();
                    repo.CreateDepartment(testdept2);
                    await repo.Save();



                    repo.PromoteEmployeeToManager(3);  //boss has emp id 3 and manager id should be 1
                    await repo.Save();
                    repo.RelateManagertoDepartment(1, 2); //should make boss a manager of jewelcrafting
                    await repo.Save();
                    repo.RelateManagertoDepartment(1, 1); //should make boss a manager of blacksmithing
                    await repo.Save();

                   

                }




                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new PhilanskiManagementSolutionsContext(options))
                {
                    var repo = new Repository(context);

                    List<int> deptsthatbobmanages = await repo.GetAllDepartmentIdsByManagerId(1);

                    Assert.Equal(2, deptsthatbobmanages.Count()); //we added bob to be the manager of two departments

                    Assert.Equal(1, deptsthatbobmanages[1]);
                    Assert.Equal(2, deptsthatbobmanages[0]);

                    context.Database.EnsureDeleted();

                }




            }







        }

    }
}
