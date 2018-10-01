using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhoIsThatServer.Storage.Context;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.Helpers
{
    public class DatabaseImageElementHelper : IDatabaseImageElementHelper
    {
        private DatabaseContextGeneration _databaseContextGeneration;

        public DatabaseImageElementHelper (DatabaseContextGeneration databaseContextGeneration = null)
        {
            //If context is null new context will be created
            _databaseContextGeneration = databaseContextGeneration ?? new DatabaseContextGeneration();
        }

        //Inherits documentation from interface
        public DatabaseImageElement InsertNewImageElement(string imageName, string imageContentUri, string personFirstName, string personLastName)
        {
            //Creates an element to insert into DB
            var imageElement = new DatabaseImageElement()
            {
                ImageName = imageName,
                ImageContentUri = imageContentUri,
                PersonFirstName = personFirstName,
                PersonLastName = personLastName
            };

            using (var context = _databaseContextGeneration.BuildDatabaseContext())
            {
                context.DatabaseImageElements.Add(imageElement);
                context.SaveChanges();
            }

            return imageElement;
        }

        public IEnumerable<DatabaseImageElement> GetAllImages()
        {
            throw new NotImplementedException();
        }
    }
}