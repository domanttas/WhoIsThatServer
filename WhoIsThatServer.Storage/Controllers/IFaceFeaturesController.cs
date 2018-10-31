using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.Controllers
{
    public interface IFaceFeaturesController
    {
        /// <summary>
        /// Posts features model into DB
        /// </summary>
        /// <param name="faceFeaturesModel">Model to insert into DB</param>
        /// <returns>Inserted object</returns>
        IHttpActionResult Post(FaceFeaturesModel faceFeaturesModel);

        /// <summary>
        /// Gets model by ID
        /// </summary>
        /// <param name="id">Person's ID</param>
        /// <returns>Model object</returns>
        IHttpActionResult GetFeatureById(int id);
    }
}
