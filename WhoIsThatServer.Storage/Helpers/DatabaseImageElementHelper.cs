using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebGrease.Css;
using WhoIsThatServer.Storage.Context;
using WhoIsThatServer.Storage.ErrorMessages;
using WhoIsThatServer.Storage.Exceptions;
using WhoIsThatServer.Storage.Models;
using WhoIsThatServer.Storage.Utils;

namespace WhoIsThatServer.Storage.Helpers
{
    public class DatabaseImageElementHelper : IDatabaseImageElementHelper
    {
        private IDatabaseContextGeneration _databaseContextGeneration;

        public DatabaseImageElementHelper (IDatabaseContextGeneration databaseContextGeneration = null)
        {
            //If context is null new context will be created
            _databaseContextGeneration = databaseContextGeneration ?? new DatabaseContextGeneration();
        }

        /// <inheritdoc/>
        public DatabaseImageElement InsertNewImageElement(int id, string imageName, string imageContentUri, string personFirstName, string personLastName, string descriptiveSentence, int score)
        {
            if (!Uri.IsWellFormedUriString(imageContentUri, UriKind.Absolute))
            {
                throw new UriFormatException("Invalid URI of image");
            }

            if (!imageName.IsFileNameValid())
            {
                throw new ArgumentException("Filename should be alphanumeric, may include -_.");
            }
            
            //Creates an element to insert into DB
            var imageElement = new DatabaseImageElement()
            {
                Id = id,
                ImageName = imageName,
                ImageContentUri = imageContentUri,
                PersonFirstName = personFirstName,
                PersonLastName = personLastName,
                DescriptiveSentence = descriptiveSentence,
                Score = score
            };

            //Inserts element into DatabaseImageElements
            using (var context = _databaseContextGeneration.BuildDatabaseContext())
            {
                if (context.DatabaseImageElements.Any(s => s.ImageContentUri == imageElement.ImageContentUri))
                    return null;
                
                context.DatabaseImageElements.Add(imageElement);
                context.SaveChanges();
            }

            return imageElement;
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public DatabaseImageElement UpdateScore(int id)
        {
            using (var context = _databaseContextGeneration.BuildDatabaseContext())
            {
                var elementToUpdate = context.DatabaseImageElements.Where(c => c.Id == id).SingleOrDefault();

                if (elementToUpdate == null)
                {
                    throw new ManagerException(StorageErrorMessages.UserDoesNotExistError);
                }

                elementToUpdate.Score++;
                context.SaveChanges();

                return elementToUpdate;
            }
        }
    }
}