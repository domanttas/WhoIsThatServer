using WhoIsThatServer.Storage.Context;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.Helpers
{
    public class HistoryModelHelper : IHistoryModelHelper
    {
        private IDatabaseContextGeneration _databaseContextGeneration;

        public HistoryModelHelper(IDatabaseContextGeneration databaseContextGeneration = null)
        {
            //If context is null new context will be created
            _databaseContextGeneration = databaseContextGeneration ?? new DatabaseContextGeneration();
        }

        /// <inheritdoc/>
        public HistoryModel InsertNewHistoryElement(int userId, int targetId, string status)
        {
            var historyModel = new HistoryModel()
            {
                UserId = userId,
                TargetId = targetId,
                Status = status
            };

            using (var context = _databaseContextGeneration.BuildDatabaseContext())
            {
                context.History.Add(historyModel);
                context.SaveChanges();
            }

            return historyModel;
        }

        /// <inheritdoc/>
        public HistoryModel GetHistoryByUserId(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}