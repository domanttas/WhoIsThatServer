using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhoIsThatServer.Recognition.ErrorMessages;
using WhoIsThatServer.Recognition.Exceptions;
using WhoIsThatServer.Recognition.Models;
using WhoIsThatServer.Recognition.Recognition;

namespace WhoIsThatServer.Recognition.Controllers
{
    [Route("api/[controller]")]
    public class RecognitionServicesController : Controller, IRecognitionServicesController
    {
        public RecognitionServices RecognitionServices { get; set; } = new RecognitionServices();

        ///<inheritdoc/>
        [HttpGet]
        [Route("identify")]
        public async Task<ActionResult> InitiateRecognition()
        {
            try
            {
                var temp = await RecognitionServices.Identify();
                return Ok(temp);
            }

            catch (ManagerException firstException) when (firstException.ErrorCode == RecognitionErrorMessages.NoFacesFoundError)
            {
                return BadRequest(RecognitionErrorMessages.NoFacesFoundError);
            }

            catch (ManagerException secondException) when (secondException.ErrorCode == RecognitionErrorMessages.NoOneIdentifiedError)
            {
                return BadRequest(RecognitionErrorMessages.NoOneIdentifiedError);
            }

            //Will change that to WrongUriError message when frontend side will be mapped to these codes
            catch (ManagerException imageException) when (imageException.ErrorCode == RecognitionErrorMessages.WrongUriError)
            {
                return BadRequest(RecognitionErrorMessages.NoOneIdentifiedError);
            }  
        }

        ///<inheritdoc/>
        [HttpPost]
        [Route("insert")]
        public async Task<ActionResult> Post([FromBody] ImageModel imageModel)
        {
            try
            {
                var result = await RecognitionServices.InsertPersonInToGroup(imageModel);
                return Ok(result);
            }

            catch (ManagerException noPersonCreatedException) when (noPersonCreatedException.ErrorCode == RecognitionErrorMessages.PersonNotCreatedError)
            {
                return BadRequest(RecognitionErrorMessages.PersonNotCreatedError);
            }
        }

        ///<inheritdoc/>
        [HttpGet]
        [Route("create")]
        public async Task<ActionResult> Create()
        {
            return Ok(await RecognitionServices.CreateGroup());
        }

        ///<inheritdoc/>
        [HttpPost]
        [Route("detect")]
        public async Task<ActionResult> DetectFeaturesOfFace([FromBody] ImageModel imageModel)
        {
            try
            {
                var result = await RecognitionServices.DetectFeaturesOfFace(imageModel);
                return Ok(result);
            }

            catch (ManagerException noFacesFoundError) when (noFacesFoundError.ErrorCode == RecognitionErrorMessages.NoFacesFoundError)
            {
                return BadRequest(RecognitionErrorMessages.NoFacesFoundError);
            }
        }
    }
}