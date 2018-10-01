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

            //Inserts element into DatabaseImageElements
            using (var context = _databaseContextGeneration.BuildDatabaseContext())
            {
                context.DatabaseImageElements.Add(imageElement);
                context.SaveChanges();
            }

            return imageElement;
        }

        /// <summary>
        /// Returns list of row objects
        /// </summary>
        /// <returns>list of DatabaseImageElement objects</returns>
        public IEnumerable<DatabaseImageElement> GetAllImages()
        {
            using (var context = _databaseContextGeneration.BuildDatabaseContext())
            {
                //LINQ syntax to select all elements from table
                var imagesList = context.DatabaseImageElements
                    .Select(s => s);

                return imagesList.ToList();
            }
        }
    }
}