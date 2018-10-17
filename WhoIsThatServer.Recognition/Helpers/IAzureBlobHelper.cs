using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhoIsThatServer.Recognition.Helpers
{
    public interface IAzureBlobHelper
    {
        /// <summary>
        /// Sets permissions of Azure Cloud storage to public
        /// </summary>
        void SetPermissionsToPublic();

        /// <summary>
        /// Returns image URI from name
        /// </summary>
        /// <param name="imageName">name of image</param>
        /// <returns>string URI</returns>
        string GetImageUri(string imageName);

        /// <summary>
        /// Deletes photo for given name (used for temp image in recognition)
        /// </summary>
        /// <param name="name">name of temp photo</param>
        void DeletePhoto(string name);
    }
}
