using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.Helpers
{
    public class DatabaseImageElementHelper : IDatabaseImageElementHelper
    {
        public DatabaseImageElement InsertNewImageElement(string ImageName, string ImageContentUri, string PersonFirstName, string PersonLastName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DatabaseImageElement> GetAllImages()
        {
            throw new NotImplementedException();
        }
    }
}