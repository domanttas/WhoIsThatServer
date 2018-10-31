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
    public class FaceFeaturesHelper : IFaceFeaturesHelper
    {
        private IDatabaseContextGeneration _databaseContextGeneration;

        public FaceFeaturesHelper(IDatabaseContextGeneration databaseContextGeneration = null)
        {
            //If context is null new context will be created
            _databaseContextGeneration = databaseContextGeneration ?? new DatabaseContextGeneration();
        }

        public FaceFeaturesModel InsertNewFeaturesModel(int personId, int age, string gender)
        {
            var element = new FaceFeaturesModel()
            {
                PersonId = personId,
                Age = age,
                Gender = gender
            };

            using (var context = _databaseContextGeneration.BuildDatabaseContext())
            {
                context.FaceFeatures.Add(element);
                context.SaveChanges();
            }

            return element;
        }

        public FaceFeaturesModel GetFaceFeaturesByPersonId(int id)
        {
            using (var context = _databaseContextGeneration.BuildDatabaseContext())
            {
                var feature = context.FaceFeatures.Where(c => c.PersonId == id).SingleOrDefault();

                if (feature == null)
                {
                    throw new ManagerException(StorageErrorMessages.UserDoesNotExistError);
                }

                return feature;
            }
        }
    }
}