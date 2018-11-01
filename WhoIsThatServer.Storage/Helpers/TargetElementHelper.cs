using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhoIsThatServer.Storage.Context;
using WhoIsThatServer.Storage.ErrorMessages;
using WhoIsThatServer.Storage.Exceptions;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.Helpers
{
    public class TargetElementHelper : ITargetElementHelper
    {
        private IDatabaseContextGeneration _databaseContextGeneration;
        private IDatabaseImageElementHelper _databaseImageElementHelper;

        public TargetElementHelper(IDatabaseContextGeneration databaseContextGeneration = null, IDatabaseImageElementHelper databaseImageElementHelper = null)
        {
            _databaseContextGeneration = databaseContextGeneration ?? new DatabaseContextGeneration();

            _databaseImageElementHelper = databaseImageElementHelper ?? new DatabaseImageElementHelper();
        }

        /// <inheritdoc/>
        public TargetElement InsertNewTargetElement(int id, int hunterPersonId, int preyPersonId, bool isHunted = false)
        {
            //Creates an element to insert in DB
            var targetElement = new TargetElement()
            {
                Id = id,
                HunterPersonId = hunterPersonId,
                PreyPersonId = preyPersonId,
                IsHunted = isHunted
            };

            //Inserts element into TargetElements
            using (var context = _databaseContextGeneration.BuildDatabaseContext())
            {
                context.TargetElements.Add(targetElement);
                context.SaveChanges();
            }

            return targetElement;
        }

        /// <inheritdoc/>
        public bool IsPreyHunted(int hunterPersonId, int preyPersonId)
        {
            using (var context = _databaseContextGeneration.BuildDatabaseContext())
            {
                try
                {
                    var tempElement = context.TargetElements.Where(c => c.HunterPersonId == hunterPersonId && c.PreyPersonId == preyPersonId).SingleOrDefault();

                    if (tempElement != null)
                    {
                        var elementToDelete = context.TargetElements.Where(c => c.Id == tempElement.Id).SingleOrDefault();

                        context.TargetElements.Remove(elementToDelete);
                        context.SaveChanges();

                        return true;
                    }

                    throw new ArgumentNullException();
                }

                catch(ArgumentNullException ex)
                {
                    throw new ManagerException(StorageErrorMessages.TargetNotFoundError);
                }
            }
        }

        public int AssignRandomTarget(int hunterPersonId)
        {
            using (var context = _databaseContextGeneration.BuildDatabaseContext())
            {
                if (context.TargetElements.Any(s => s.HunterPersonId == hunterPersonId))
                {
                    throw new ManagerException(StorageErrorMessages.TargetAlreadyAssignedError);
                }

                var users = _databaseImageElementHelper.GetAllImages();

                if (users.Count() < 2)
                    throw new ManagerException(StorageErrorMessages.ThereAreNoPlayersError);

                Random random = new Random();

                try
                {
                    var index = hunterPersonId;

                    while (users.ElementAt(index).Id == hunterPersonId)
                    {
                        index = random.Next(0, users.Count());
                    }

                    var result = InsertNewTargetElement(0, hunterPersonId, users.ElementAt(index).Id);

                    return result.PreyPersonId;
                }

                catch(ArgumentNullException argumentNullException)
                {
                    throw new ManagerException(StorageErrorMessages.ThereAreNoPlayersError);
                }

                catch(ArgumentOutOfRangeException argumentOutOfRangeException)
                {
                    throw new ManagerException(StorageErrorMessages.TargetNotAssignedError);
                }
            }
        }
    }
}