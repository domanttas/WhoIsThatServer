using System;
using System.Collections.Generic;
using System.Linq;
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
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Testing Azure Blob Controller
            AzureBlobController azureBlobController = new AzureBlobController();
            string firstTest = azureBlobController.GetImageUri("Domantas_test.jpg");

            //Inserting dummy data for controller testing
            DatabaseImageElement databaseImageElement = new DatabaseImageElement()
            {
                Id = 1,
                ImageName = "Domantas_test.jpg",
                ImageContentUri = firstTest,
                PersonFirstName = "Domantas",
                PersonLastName = "WorkPls"
            };
            DatabaseImageElementController test = new DatabaseImageElementController();
            test.Post(databaseImageElement);
        }
    }
}
