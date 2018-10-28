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

        public TargetElementHelper(IDatabaseContextGeneration databaseContextGeneration = null)
        {
            //If context is null new context will be created
            _databaseContextGeneration = databaseContextGeneration ?? new DatabaseContextGeneration();
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
    }
}