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
        IHttpActionResult Post(DatabaseImageElement databaseImageElement);
        
        /// <summary>
        /// Updates user's score
        /// </summary>
        /// <param name="databaseImageElement">Element for which score is updated</param>
        /// <returns>Updated element</returns>
        IHttpActionResult UpdateScore(int id);

        /// <summary>
        /// Gets user by ID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User object</returns>
        IHttpActionResult GetUserById(int id);
    }
}