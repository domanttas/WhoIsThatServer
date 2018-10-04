using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhoIsThatServer.Storage.Controllers
{
    public interface IAzureBlobController
    {
        /// <summary>
        /// Returns image uri from Azure Blob storage by image name
        /// </summary>
        /// <param name="imageName"></param>
        /// <returns>Uri</returns>
        string GetImageUri(string imageName);
    }
}