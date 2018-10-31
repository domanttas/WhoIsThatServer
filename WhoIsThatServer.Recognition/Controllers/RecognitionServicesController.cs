﻿using System;
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

            catch (ManagerException firstException) when (firstException.ErrorCode == RecognitionErrorMessages.NoFacesFoundError)
            {
                return Json(RecognitionErrorMessages.NoFacesFoundError);
            }

            catch (ManagerException secondException) when (secondException.ErrorCode == RecognitionErrorMessages.NoOneIdentifiedError)
            {
                return Json(RecognitionErrorMessages.NoOneIdentifiedError);
            }

            //Will change that to WrongUriError message when frontend side will be mapped to these codes
            catch (ManagerException imageException) when (imageException.ErrorCode == RecognitionErrorMessages.WrongUriError)
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

            catch (ManagerException noPersonCreatedException) when (noPersonCreatedException.ErrorCode == RecognitionErrorMessages.PersonNotCreatedError)
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

        [HttpGet]
        [Route("detect")]
        public async Task<JsonResult> DetectFeaturesOfFace([FromBody] ImageModel imageModel)
        {
            try
            {
                var result = await RecognitionServices.DetectFeaturesOfFace(imageModel);
                return Json(result);
            }

            catch (ManagerException noFacesFoundError) when (noFacesFoundError.ErrorCode == RecognitionErrorMessages.NoFacesFoundError)
            {
                return Json(RecognitionErrorMessages.NoFacesFoundError);
            }
        }
    }
}