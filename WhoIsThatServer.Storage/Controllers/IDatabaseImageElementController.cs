using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WhoIsThatServer.Storage.Helpers;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.Controllers
{
    public interface IDatabaseImageElementController
    {
        /// <summary>
        /// Gets all obejcts from database table 'DatabaseImageElements'
        /// </summary>
        /// <returns>Json list</returns>
        [HttpPost]
        IHttpActionResult GetAllImages();

        /// <summary>
        /// Adds DatabaseImageElement object to DB
        /// </summary>
        /// <param name="databaseImageElement">Element to insert</param>
        /// <returns>Json of inserted element</returns>
        IHttpActionResult Post([FromBody] DatabaseImageElement databaseImageElement);
    }
}