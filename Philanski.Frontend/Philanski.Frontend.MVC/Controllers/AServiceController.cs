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

namespace Philanski.Frontend.MVC.Controllers
{
    public class AServiceController : Controller
    {

<<<<<<< HEAD
    //    private static readonly Uri s_serviceUri = new Uri("https://localhost:44386/");

        private static readonly Uri s_serviceUri = new Uri("https://philanksi.azurewebsites.net/");
        protected static readonly string s_CookieName = "PhilanskiApiAuth";
=======
       //private static readonly Uri s_serviceUri = new Uri("https://localhost:44386/");
     private static readonly Uri s_serviceUri = new Uri("https://philanksi.azurewebsites.net/");
        protected static readonly string s_CookieName = "PhilanskiApiAuth2";
>>>>>>> 79b2853ed98649f88cbbc73f5f7587fb9ff48c16

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
    }
}