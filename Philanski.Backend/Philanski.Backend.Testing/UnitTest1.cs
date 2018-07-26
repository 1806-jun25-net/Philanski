using Moq;
using Philanski.Backend.Library.Models;
using Philanski.Backend.Library.Repositories;
using System;
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
            var TimeSheetApp = new TimeSheetApproval();
            var testDate = Convert.ToDateTime(test);
            var result = TimeSheetApp.GetPreviousSundayOfWeek(testDate);
            Assert.Equal(Convert.ToDateTime(actual), result);

        }

        [Theory]
        [InlineData("07/23/2018 14:50:50.42", "07/28/2018 14:50:50.42")]
        [InlineData("07/28/2018 14:50:50.42", "07/28/2018 14:50:50.42")]
        public void GetNextSaturdayTest(string test, string actual)
        {
            var TimeSheetApp = new TimeSheetApproval();
            var testDate = Convert.ToDateTime(test);
            var result = TimeSheetApp.GetNextSaturdayOfWeek(testDate);
            Assert.Equal(Convert.ToDateTime(actual), result);

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
