using System.Web.Http;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.Controllers
{
    public interface IHistoryElementController
    {
        /// <summary>
        /// Calls helper which posts history element into DB
        /// </summary>
        /// <param name="historyModel">Model to insert</param>
        /// <returns>Inserted model</returns>
        IHttpActionResult Post([FromBody] HistoryModel historyModel);

        /// <summary>
        /// Calls helper which gets history element by user ID from DB
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>History model</returns>
        IHttpActionResult GetElementById(int userId);

        /// <summary>
        /// Updates history element
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Updated element</returns>
        IHttpActionResult UpdateHistoryElement(int userId);
    }
}