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
            try
            {
                return Json(HistoryModelHelper.GetHistoryByUserId(userId));
            }

            catch (ManagerException historyNotFoundException) when (historyNotFoundException.ErrorCode ==
                                                                    StorageErrorMessages.HistoryElementNotFoundError)
            {
                return BadRequest(StorageErrorMessages.HistoryElementNotFoundError);
            }
        }

        /// <inheritdoc/>
        [HttpGet]
        [Route("api/history/update/{userId}")]
        public IHttpActionResult UpdateHistoryElement(int userId)
        {
            try
            {
                return Json(HistoryModelHelper.UpdateHistoryModel(userId));
            }

            catch (ManagerException userDoesNotExistsError) when (userDoesNotExistsError.ErrorCode ==
                                                                  StorageErrorMessages.UserDoesNotExistError)
            {
                return BadRequest(StorageErrorMessages.UserDoesNotExistError);
            }
        }
    }
}