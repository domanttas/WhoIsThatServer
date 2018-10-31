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
    public class FaceFeaturesController : ApiController
    {
        public IFaceFeaturesHelper FaceFeaturesHelper { get; set; } = new FaceFeaturesHelper();

        [HttpPost]
        [Route("api/features/add")]
        public IHttpActionResult Post([FromBody] FaceFeaturesModel faceFeaturesModel)
        {
            return Json(FaceFeaturesHelper.InsertNewFeaturesModel(faceFeaturesModel.PersonId, faceFeaturesModel.Age, faceFeaturesModel.Gender));
        }

        [HttpGet]
        [Route("api/features/get/{id}")]
        public IHttpActionResult GetFeatureById(int id)
        {
            try
            {
                var result = FaceFeaturesHelper.GetFaceFeaturesByPersonId(id);
                return Json(result);
            }

            catch (ManagerException featureNotFoundException) when (featureNotFoundException.ErrorCode == StorageErrorMessages.UserDoesNotExistError)
            {
                return Json(StorageErrorMessages.UserDoesNotExistError);
            }
        }
    }
}