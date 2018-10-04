using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.Helpers
{
    public interface IDatabaseImageElementHelper
    {
        /// <summary>
        /// Creates and inserts DatabaseImageElement into ImageElement table
        /// </summary>
        /// <param name="ImageName">Name of the image</param>
        /// <param name="ImageContentUri">Uri of the image in storage</param>
        /// <param name="PersonFirstName">First name</param>
        /// <param name="PersonLastName">Last name</param>
        /// <returns>Inserted object's instance</returns>
        DatabaseImageElement InsertNewImageElement(int id, string ImageName, string ImageContentUri, string PersonFirstName, string PersonLastName);

        /// <summary>
        /// Gets all images from DB
        /// </summary>
        /// <returns>Returns list of images from DB</returns>
        IEnumerable<DatabaseImageElement> GetAllImages();
    }
}
