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

            string firstTest = azureBlobController.GetImageUri("Domantas_test_2.jpg");
            DatabaseImageElement databaseImageElement = new DatabaseImageElement()
            {
                Id = 1,
                ImageName = "Domantas_test_2.jpg",
                ImageContentUri = firstTest,
                PersonFirstName = "Domantas",
                PersonLastName = "WorkPls"
            };
            test.Post(databaseImageElement);

            string secondTest = azureBlobController.GetImageUri("Luktas_test.jpg");
            DatabaseImageElement databaseImageElementSec = new DatabaseImageElement()
            {
                Id = 2,
                ImageName = "Luktas_test.jpg",
                ImageContentUri = secondTest,
                PersonFirstName = "Lukas",
                PersonLastName = "Elenbergas"
            };
            test.Post(databaseImageElementSec);
            //string restUrl = "https://testrecognition.azurewebsites.net/api/recognitionservices/insert";
            //HttpResponseMessage response = await client.PostAsJsonAsync(
            //    restUrl, databaseImageElementSec);
        }
    }
}
