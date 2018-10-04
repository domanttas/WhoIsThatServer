using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WhoIsThatServer.Storage.Controllers;
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

            //Inserting dummy data for controller testing
            DatabaseImageElement t = new DatabaseImageElement()
            {
                Id = 1,
                ImageName = "test",
                ImageContentUri = "test",
                PersonFirstName = "test",
                PersonLastName = "test"
            };
            DatabaseImageElementController test = new DatabaseImageElementController();
            test.Post(t);
        }
    }
}
