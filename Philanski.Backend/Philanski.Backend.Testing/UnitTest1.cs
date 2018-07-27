using Microsoft.AspNetCore.Mvc;
using Moq;
using Philanski.Backend.Library.Models;
using Philanski.Backend.Library.Repositories;
using Philanski.Backend.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Philanski.Backend.Testing
{
    public class UnitTest1
    {




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
            var view = Assert.IsType<ViewResult>(actionResult);
            var model = Assert.IsAssignableFrom<List<Department>>
                (view.Model).ToList();
            Assert.Equal(deptList.Count, model.Count);
            for (int i = 0; i < model.Count; i++)
            {
                Assert.Equal(deptList[i].Id, model[i].Id);
                Assert.Equal(deptList[i].Name, model[i].Name);
            }
        }





        [Fact]
        public void GetEmployeeByIDRepoMethodShouldWork()
        {
            // arrange
            Employee employeebyID = new Employee();
            employeebyID.Id = 1;
            employeebyID.FirstName = "Phil";
            employeebyID.LastName = "Platt";
            employeebyID.Email = "bobman56@gmail.com";
            employeebyID.JobTitle = "Pancake Flipper";
            employeebyID.WorksiteId = 3;
            employeebyID.Salary = 45000.00m;
            employeebyID.HireDate = DateTime.Now;
           
                

            var mockRepo = new Mock<Repository>();
            mockRepo.Setup(x => x.GetEmployeeByID(1)).Returns(employeebyID);

           
               
            

        }







    }
}
