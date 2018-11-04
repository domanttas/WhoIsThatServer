﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WhoIsThatServer.Storage.Context;
using WhoIsThatServer.Storage.ErrorMessages;
using WhoIsThatServer.Storage.Exceptions;
using WhoIsThatServer.Storage.Helpers;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.Controllers
{
    public class TargetElementController : ApiController, ITargetElementController
    {
        public ITargetElementHelper TargetElementHelper { get; set; } = new TargetElementHelper();
        public IDatabaseContextGeneration DatabaseContextGeneration { get; set; } = new DatabaseContextGeneration();

        /// <inheritdoc/>
        [HttpDelete]
        [Route("api/game/remove")]
        public IHttpActionResult IsPreyHunted([FromBody] TargetElement targetElement)
        {
            try
            {
                return Json(TargetElementHelper.IsPreyHunted(targetElement.HunterPersonId, targetElement.PreyPersonId));
            }

            catch (ManagerException targetNotFoundException) when (targetNotFoundException.ErrorCode ==
                                                                   StorageErrorMessages.TargetNotFoundError)
            {
                return Json(StorageErrorMessages.TargetNotFoundError);
            }
        }

        /// <inheritdoc/>
        [HttpPost]
        [Route("api/game/add")]
        public IHttpActionResult Post([FromBody] TargetElement targetElement)
        {
            targetElement = TargetElementHelper.InsertNewTargetElement(targetElement.Id, targetElement.HunterPersonId, targetElement.PreyPersonId, targetElement.IsHunted);

            return Json(targetElement);
        }

        /// <inheritdoc/>
        [HttpPost]
        [Route("api/game/{id}")]
        public IHttpActionResult AssignRandomTarget(int id)
        {
            try
            {
                return Json(TargetElementHelper.AssignRandomTarget(id));
            }

            catch (ManagerException targetExistsException) when (targetExistsException.ErrorCode == StorageErrorMessages.TargetAlreadyAssignedError)
            {
                return Json(StorageErrorMessages.TargetAlreadyAssignedError);
            }

            catch (ManagerException noPlayersException) when (noPlayersException.ErrorCode == StorageErrorMessages.ThereAreNoPlayersError)
            {
                return Json(StorageErrorMessages.ThereAreNoPlayersError);
            }

            catch (ManagerException targetNotAssignedException) when (targetNotAssignedException.ErrorCode == StorageErrorMessages.TargetNotAssignedError)
            {
                return Json(StorageErrorMessages.TargetNotAssignedError);
            }
        }
    }
}