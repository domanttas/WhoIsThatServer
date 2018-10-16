using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WhoIsThatServer.Storage.Recognition;

namespace WhoIsThatServer.Storage.Controllers
{
    public class RecognitionServicesController : ApiController
    {
        public RecognitionServices RecognitionServices { get; set; } = new RecognitionServices();

        [HttpGet]
        [Route("api/recognition")]
        public IHttpActionResult InitiateRecognition()
        {
            RecognitionServices.CreateGroup();
            return Json(RecognitionServices.Identify());
        }
    }
}