using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WhoIsThatServer.Storage.Helpers;

namespace WhoIsThatServer.Storage.Controllers
{
    public class AzureBlobController : ApiController, IAzureBlobController
    {
        public AzureBlobHelper AzureBlobHelper { get; set; } = new AzureBlobHelper();

        public AzureBlobController()
        {
            AzureBlobHelper.SetPermissionsToPublic();
        }

        [HttpGet]
        [Route("api/blob/get_uri")]
        public string GetImageUri(string imageName)
        {
            return AzureBlobHelper.GetImageUri(imageName);
        }
    }
}