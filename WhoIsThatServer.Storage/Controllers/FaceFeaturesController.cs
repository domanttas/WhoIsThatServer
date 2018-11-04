using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WhoIsThatServer.Storage.ErrorMessages;
using WhoIsThatServer.Storage.Exceptions;
using WhoIsThatServer.Storage.Helpers;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.Controllers
{
    public class FaceFeaturesController : ApiController, IFaceFeaturesController
    {
        public IFaceFeaturesHelper FaceFeaturesHelper { get; set; } = new FaceFeaturesHelper();

        /// <inheritdoc/>
        [HttpPost]
        [Route("api/features")]
        public IHttpActionResult Post([FromBody] FaceFeaturesModel faceFeaturesModel)
        {
            return Json(FaceFeaturesHelper.InsertNewFeaturesModel(faceFeaturesModel.PersonId, faceFeaturesModel.Age, faceFeaturesModel.Gender));
        }

        /// <inheritdoc/>
        [HttpGet]
        [Route("api/features/{id}")]
        public IHttpActionResult GetFeatureById(int id)
        {
            try
            {
                var result = FaceFeaturesHelper.GetFaceFeaturesByPersonId(id);
                return Json(result);
            }

            catch (ManagerException featureNotFoundException) when (featureNotFoundException.ErrorCode == StorageErrorMessages.UserDoesNotExistError)
            {
                return BadRequest(StorageErrorMessages.UserDoesNotExistError);
            }
        }
    }
}