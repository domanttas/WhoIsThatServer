using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WhoIsThatServer.Storage.Context;
using WhoIsThatServer.Storage.Helpers;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.Controllers
{
    public class TargetElementController : ApiController, ITargetElementController
    {
        public ITargetElementHelper TargetElementHelper { get; set; } = new TargetElementHelper();
        public IDatabaseContextGeneration DatabaseContextGeneration { get; set; } = new DatabaseContextGeneration();

        /// <inheritdoc/>
        [HttpDelete]
        [Route("api/game/remove")]
        public IHttpActionResult IsPreyHunted([FromBody] TargetElement targetElement)
        {
            return Json(TargetElementHelper.IsPreyHunted(targetElement.HunterPersonId, targetElement.PreyPersonId));
        }

        /// <inheritdoc/>
        [HttpPost]
        [Route("api/game/add")]
        public IHttpActionResult Post([FromBody] TargetElement targetElement)
        {
            targetElement = TargetElementHelper.InsertNewTargetElement(targetElement.Id, targetElement.HunterPersonId, targetElement.PreyPersonId, targetElement.IsHunted);

            return Json(targetElement);
        }
    }
}