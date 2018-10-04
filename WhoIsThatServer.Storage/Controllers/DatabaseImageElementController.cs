using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WhoIsThatServer.Storage.Context;
using WhoIsThatServer.Storage.Helpers;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.Controllers
{
    public class DatabaseImageElementController : ApiController, IDatabaseImageElementController
    {
        public IDatabaseImageElementHelper DatabaseImageElementHelper { get; set; } = new DatabaseImageElementHelper();
        public DatabaseContextGeneration DatabaseContextGeneration { get; set; } = new DatabaseContextGeneration();

        [HttpGet]
        public IHttpActionResult GetAllImages()
        {
            return Json(DatabaseImageElementHelper.GetAllImages());
        }

        public IHttpActionResult Post([FromBody] DatabaseImageElement databaseImageElement)
        {
            using (var context = DatabaseContextGeneration.BuildDatabaseContext())
            {
                context.DatabaseImageElements.Add(databaseImageElement);
                context.SaveChanges();
            }

            return Json(databaseImageElement);
        }
    }
}