using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhoIsThatServer.Recognition.Models;
using WhoIsThatServer.Recognition.Recognition;

namespace WhoIsThatServer.Recognition.Controllers
{
    [Route("api/[controller]")]
    public class RecognitionServicesController : Controller, IRecognitionServicesController
    {
        public RecognitionServices RecognitionServices { get; set; } = new RecognitionServices();

        //////<inheritdoc/>
        [HttpGet]
        [Route("identify")]
        public async Task<JsonResult> InitiateRecognition()
        {
            var temp = await RecognitionServices.Identify();
            return Json(temp);
        }

        [HttpPost]
        [Route("insert")]
        public async Task<JsonResult> Post([FromBody] ImageModel imageModel)
        {
            var result = await RecognitionServices.InsertPersonInToGroup(imageModel);
            return Json(result);
        }

        [HttpGet]
        [Route("create")]
        public async Task<JsonResult> Create()
        {
            return Json(await RecognitionServices.CreateGroup());
        }
    }
}