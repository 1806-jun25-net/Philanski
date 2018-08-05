using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Philanski.Frontend.MVC.Models;

namespace Philanski.Frontend.MVC.Controllers
{
    public class AServiceController : Controller
    {


       private static readonly Uri s_serviceUri = new Uri("https://localhost:44386/");
        //private static readonly Uri s_serviceUri = new Uri("https://philanksi.azurewebsites.net/");
        protected static readonly string s_CookieName = "PhilanskiApiAuth2";


        protected HttpClient HttpClient { get; }

        public AServiceController(HttpClient httpClient)
        {
            // don't forget to register HttpClient as a singleton service in Startup.cs,
            // with the right HttpClientHandler
            HttpClient = httpClient;
        }

        protected HttpRequestMessage CreateRequestToService(HttpMethod method, string uri, object body = null)
        {
            var apiRequest = new HttpRequestMessage(method, new Uri(s_serviceUri, uri));

            if (body != null)
            {
                string jsonString = JsonConvert.SerializeObject(body);
                apiRequest.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            }

            string cookieValue = Request.Cookies[s_CookieName];

            if (cookieValue != null)
            {
                apiRequest.Headers.Add("Cookie", new CookieHeaderValue(s_CookieName, cookieValue).ToString());
            }

            return apiRequest;
        }
       /* protected async Task<Employees> GetCurrentEmployeeDataHttpRequest()
        {
            var uri = "api/employee";
            var request = CreateRequestToService(HttpMethod.Get, uri);

            var response = await HttpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            string jsonStringUserName = await responseUsername.Content.ReadAsStringAsync();
            List<Employees> Employee = JsonConvert.DeserializeObject<List<Employees>>(jsonStringUserName);
            
        }*/
    }
}