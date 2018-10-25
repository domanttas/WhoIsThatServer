using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.Helpers
{
    public interface ITargetElementHelper
    {
        /// <summary>
        /// Inserts new target element into DB
        /// </summary>
        /// <param name="id">Unique ID</param>
        /// <param name="hunterPersonId">ID of person who is hunting</param>
        /// <param name="preyPersonId">ID of target</param>
        /// <param name="isHunted">Boolean value whether target is hunter, by default it is false</param>
        /// <returns>Inserted element</returns>
        TargetElement InsertNewTargetElement(int id, int hunterPersonId, int preyPersonId, bool isHunted);

        /// <summary>
        /// Checks if hunter got the right target
        /// </summary>
        /// <param name="hunterPersonId">ID of person who is hunting</param>
        /// <param name="preyPersonId">ID of target</param>
        /// <returns>Boolean</returns>
        bool IsPreyHunted(int hunterPersonId, int preyPersonId);
    }
}
