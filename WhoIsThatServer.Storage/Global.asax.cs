using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WhoIsThatServer.Storage.Controllers;
using WhoIsThatServer.Storage.Helpers;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected async void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            //Testing Azure Blob Controller
            AzureBlobController azureBlobController = new AzureBlobController();
            DatabaseImageElementController test = new DatabaseImageElementController();
            HttpClient client = new HttpClient();

            /*
            string firstTest = azureBlobController.GetImageUri("Domantas_test_2.jpg");
            DatabaseImageElement databaseImageElement = new DatabaseImageElement()
            {
                ImageName = "Domantas_test_2.jpg",
                ImageContentUri = firstTest,
                PersonFirstName = "Domantas",
                PersonLastName = "WorkPls",
                DescriptiveSentence = "Very descriptive sentence",
                Score = 0
            };
            test.Post(databaseImageElement);
            //string firstRestUrl = "https://testrecognition.azurewebsites.net/api/recognitionservices/insert";
            //HttpResponseMessage firstResponse = await client.PostAsJsonAsync(
            //    firstRestUrl, databaseImageElement);

            string secondTest = azureBlobController.GetImageUri("Luktas_test.jpg");
            DatabaseImageElement databaseImageElementSec = new DatabaseImageElement()
            {
                ImageName = "Luktas_test.jpg",
                ImageContentUri = secondTest,
                PersonFirstName = "Lukas",
                PersonLastName = "Elenbergas",
                DescriptiveSentence = "Something bad",
                Score = 0
            };
            test.Post(databaseImageElementSec);
            //string restUrl = "https://testrecognition.azurewebsites.net/api/recognitionservices/insert";
            //HttpResponseMessage response = await client.PostAsJsonAsync(
            //    restUrl, databaseImageElementSec);

            //string restUrl = "https://testrecognition.azurewebsites.net/api/recognitionservices/create";
            //HttpResponseMessage response = await client.GetAsync(
            //    restUrl);
            */
        }
    }
}
