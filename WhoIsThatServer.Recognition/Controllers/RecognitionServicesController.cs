using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhoIsThatServer.Recognition.ErrorMessages;
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
            try
            {
                var temp = await RecognitionServices.Identify();
                return Json(temp);
            }

            catch (IndexOutOfRangeException indexOutOfRangeException)
            {
                return Json(RecognitionErrorMessages.NoFacesFoundError);
            }

            catch (ArgumentNullException argumentNullException)
            {
                return Json(RecognitionErrorMessages.NoOneIdentifiedError);
            }
        }

        [HttpPost]
        [Route("insert")]
        public async Task<JsonResult> Post([FromBody] ImageModel imageModel)
        {
            try
            {
                var result = await RecognitionServices.InsertPersonInToGroup(imageModel);
                return Json(result);
            }

            catch (ArgumentNullException argumentNullException)
            {
                return Json(RecognitionErrorMessages.PersonNotCreatedError);
            }
        }

        [HttpGet]
        [Route("create")]
        public async Task<JsonResult> Create()
        {
            return Json(await RecognitionServices.CreateGroup());
        }
    }
}