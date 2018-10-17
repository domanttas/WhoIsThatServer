using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhoIsThatServer.Recognition.Recognition;

namespace WhoIsThatServer.Recognition.Controllers
{
    [Route("api/[controller]")]
    public class RecognitionServicesController : Controller
    {
        public RecognitionServices RecognitionServices { get; set; } = new RecognitionServices();

        [HttpGet]
        [Route("identify")]
        public async Task<JsonResult> InitiateRecognition()
        {
            var temp = await RecognitionServices.Identify();
            return Json(temp);
        }
    }
}