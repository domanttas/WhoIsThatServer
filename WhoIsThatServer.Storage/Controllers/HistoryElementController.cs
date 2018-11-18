using System.Web.Http;
using WhoIsThatServer.Storage.ErrorMessages;
using WhoIsThatServer.Storage.Exceptions;
using WhoIsThatServer.Storage.Helpers;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.Controllers
{
    public class HistoryElementController : ApiController, IHistoryElementController
    {
        public IHistoryModelHelper HistoryModelHelper { get; set; } = new HistoryModelHelper();
        
        /// <inheritdoc/>
        [HttpPost]
        [Route("api/history")]
        public IHttpActionResult Post([FromBody] HistoryModel historyModel)
        {
            return Json(HistoryModelHelper.InsertNewHistoryElement(historyModel.UserId, historyModel.TargetId, historyModel.Status));
        }
        
        /// <inheritdoc/>
        [HttpGet]
        [Route("api/history/{userId}")]
        public IHttpActionResult GetElementById(int userId)
        {
            return Json(HistoryModelHelper.GetHistoryByUserId(userId));
        }

        /// <inheritdoc/>
        [HttpGet]
        [Route("api/history/update/{userId}/{targetId}")]
        public IHttpActionResult UpdateHistoryElement(int userId, int targetId)
        {
            try
            {
                return Json(HistoryModelHelper.UpdateHistoryModel(userId, targetId));
            }

            catch (ManagerException userDoesNotExistsError) when (userDoesNotExistsError.ErrorCode ==
                                                                  StorageErrorMessages.UserDoesNotExistError)
            {
                return BadRequest(StorageErrorMessages.UserDoesNotExistError);
            }
        }
    }
}