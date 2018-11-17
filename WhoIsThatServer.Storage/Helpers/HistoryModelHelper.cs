using System.Collections.Generic;
using System.Linq;
using WhoIsThatServer.Storage.Constants;
using WhoIsThatServer.Storage.Context;
using WhoIsThatServer.Storage.ErrorMessages;
using WhoIsThatServer.Storage.Exceptions;
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
        public IEnumerable<HistoryModel> GetHistoryByUserId(int userId)
        {
            using (var context = _databaseContextGeneration.BuildDatabaseContext())
            {
                var historyModel = context.History.Where(c => c.UserId == userId).ToList();

                if (historyModel == null)
                {
                    throw new ManagerException(StorageErrorMessages.HistoryElementNotFoundError);
                }

                return historyModel;
            }
        }

        /// <inheritdoc/>
        public HistoryModel UpdateHistoryModel(int userId)
        {
            using (var context = _databaseContextGeneration.BuildDatabaseContext())
            {
                var element = context.History.Where(c => c.UserId == userId).ToList();

                if (element.Count != 1)
                {
                    throw new ManagerException(StorageErrorMessages.UserDoesNotExistError);
                }

                element.ElementAt(0).Status = StatusConstants.TargetHuntedHistory;
                context.SaveChanges();

                return element.ElementAt(0);
            }
        }
    }
}