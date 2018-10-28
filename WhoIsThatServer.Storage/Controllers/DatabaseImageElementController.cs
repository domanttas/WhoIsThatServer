using System;
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
    public class DatabaseImageElementController : ApiController, IDatabaseImageElementController
    {
        public IDatabaseImageElementHelper DatabaseImageElementHelper { get; set; } = new DatabaseImageElementHelper();
        public IDatabaseContextGeneration DatabaseContextGeneration { get; set; } = new DatabaseContextGeneration();

        /// <inheritdoc/>
        [HttpGet]
        [Route("api/images/all")]
        public IHttpActionResult GetAllImages()
        {
            return Json(DatabaseImageElementHelper.GetAllImages());
        }

        /// <inheritdoc/>
        [HttpPost]
        [Route("api/images/add")]
        public IHttpActionResult Post([FromBody] DatabaseImageElement databaseImageElement)
        {
            try
            {
                databaseImageElement = DatabaseImageElementHelper.InsertNewImageElement(databaseImageElement.Id,
                    databaseImageElement.ImageName, databaseImageElement.ImageContentUri,
                    databaseImageElement.PersonFirstName, databaseImageElement.PersonLastName,
                    databaseImageElement.DescriptiveSentence, databaseImageElement.Score);

                return Json(databaseImageElement);
            }

            catch (ManagerException wrongUriException) when (wrongUriException.ErrorCode ==
                                                             StorageErrorMessages.InvalidImageUriError)
            {
                return Json(StorageErrorMessages.InvalidImageUriError);
            }

            catch (ManagerException wrongFilenameException) when (wrongFilenameException.ErrorCode ==
                                                                  StorageErrorMessages.InvalidFileNameError)
            {
                return Json(StorageErrorMessages.InvalidFileNameError);
            }
        }

        /// <inheritdoc/>
        [HttpPut]
        [Route("api/images/score")]
        public IHttpActionResult UpdateScore([FromBody] DatabaseImageElement databaseImageElement)
        {
            try
            {
                return Json(DatabaseImageElementHelper.UpdateScore(databaseImageElement.Id));
            }

            catch (ManagerException userNotFoundException) when (userNotFoundException.ErrorCode ==
                                                                 StorageErrorMessages.UserDoesNotExistError)
            {
                return Json(StorageErrorMessages.UserDoesNotExistError);
            }
        }

        /// <inheritdoc/>
        [HttpGet]
        [Route("api/images/user/{id}")]
        public IHttpActionResult GetUserById(int id)
        {
            try
            {
                return Json(DatabaseImageElementHelper.GetUserById(id));
            }

            catch (ManagerException userNotFoundException) when (userNotFoundException.ErrorCode ==
                                                                 StorageErrorMessages.UserDoesNotExistError)
            {
                return Json(StorageErrorMessages.UserDoesNotExistError);
            }
        }
    }
}