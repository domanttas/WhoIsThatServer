using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.Helpers
{
    public interface IHistoryModelHelper
    {
        /// <summary>
        /// Inserts new history element into table
        /// </summary>
        /// <param name="userId">ID of user</param>
        /// <param name="targetId">ID of target</param>
        /// <param name="status">Status of game</param>
        /// <returns>Inserted element</returns>
        HistoryModel InsertNewHistoryElement(int userId, int targetId, string status);
        
        /// <summary>
        /// Gets element by user ID
        /// </summary>
        /// <param name="userId">ID of user</param>
        /// <returns>HistoryModel element</returns>
        HistoryModel GetHistoryByUserId(int userId);

        /// <summary>
        /// Updates status of model
        /// </summary>
        /// <param name="userId">ID of user</param>
        /// <returns>Updated model</returns>
        HistoryModel UpdateHistoryModel(int userId);
    }
}