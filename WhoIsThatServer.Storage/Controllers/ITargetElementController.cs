using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.Controllers
{
    public interface ITargetElementController
    {
        /// <summary>
        /// Adds created target element into DB
        /// </summary>
        /// <param name="targetElement">object with id, target's and hunter's ids</param>
        /// <returns>Json</returns>
        IHttpActionResult Post([FromBody] TargetElement targetElement);

        /// <summary>
        /// Double - checks if correct target was hunter and removes it from TargetElements table
        /// </summary>
        /// <param name="targetElement">Object with ids</param>
        /// <returns>Boolean as Json</returns>
        IHttpActionResult IsPreyHunted(TargetElement targetElement);

        /// <summary>
        /// Assigns random target
        /// </summary>
        /// <param name="id">ID of hunter</param>
        /// <returns>Assigned prey's ID</returns>
        IHttpActionResult AssignRandomTarget(int id);

        /// <summary>
        /// Gets target by hunter id
        /// </summary>
        /// <param name="id">Id of hubter</param>
        /// <returns>Target object</returns>
        IHttpActionResult GetTargetById(int id);
    }
}
