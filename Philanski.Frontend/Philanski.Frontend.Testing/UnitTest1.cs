using Philanski.Frontend.Library.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;
using Moq;
using Moq.Protected;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using Philanski.Frontend.MVC.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Philanski.Frontend.Testing
{
    public class UnitTest1
    {
        [Fact]
        public void TestingDepartmentController()
        {
      

                // Arrange 2
                //const string testContent = "test content";
                var mockMessageHandler = new Mock<HttpMessageHandler>();


            //below was from example online but I couldn't make much sense of it. consider deleting. test passes without it. 
                //mockMessageHandler.Protected()
                //    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                //    .Returns(Task.FromResult(new HttpResponseMessage
                //    {
                //        StatusCode = HttpStatusCode.OK,
                //        Content = new StringContent(testContent)
                //    }));


            HttpClient client = new HttpClient(mockMessageHandler.Object);

            DepartmentController controller = new DepartmentController(client);


            // Act
            var result = controller.Index();

            // Assert
           Assert.IsAssignableFrom<Task<ActionResult>>(result);
            Assert.Equal(1, 1);



        }

        [Fact]
        public void TestingEmployeeController()
        {
           
            // Arrange 2
            var mockMessageHandler = new Mock<HttpMessageHandler>();
          

            HttpClient client = new HttpClient(mockMessageHandler.Object);

            EmployeeController controller = new EmployeeController(client);


            // Act
            var result = controller.Index();

            // Assert
            Assert.IsAssignableFrom<Task<ActionResult>>(result);
            Assert.Equal(1, 1);



        }

        [Fact]
        public void TestingAccountController()
        {

            // Arrange 2
            var mockMessageHandler = new Mock<HttpMessageHandler>();


            HttpClient client = new HttpClient(mockMessageHandler.Object);

            AccountController controller = new AccountController(client);


            // Act
            var result = controller.Register();

            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
            Assert.Equal(1, 1);



        }


        [Fact]
        public void TestingAServiceController()
        {

            // Arrange 2
            var mockMessageHandler = new Mock<HttpMessageHandler>();


            HttpClient client = new HttpClient(mockMessageHandler.Object);

            AServiceController controller = new AServiceController(client);


            // Act
            Assert.NotNull(controller);



        }

        [Fact]
        public void TestingHomeController()
        {

            // Arrange 2
            var mockMessageHandler = new Mock<HttpMessageHandler>();


            HttpClient client = new HttpClient(mockMessageHandler.Object);

            HomeController controller = new HomeController(client);


            // Act
            var result = controller.Index();

            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
            Assert.Equal(1, 1);



        }

        [Fact]
        public void TestingTimeSheetController()
        {

            // Arrange 2
            var mockMessageHandler = new Mock<HttpMessageHandler>();


            HttpClient client = new HttpClient(mockMessageHandler.Object);

            TimeSheetController controller = new TimeSheetController(client);


            // Act
            var result = controller.Index();

            // Assert
            Assert.IsAssignableFrom<Task<ActionResult>>(result);
            Assert.Equal(1, 1);



        }




    }
}
