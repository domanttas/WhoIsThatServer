using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhoIsThatServer.Recognition.Models;

namespace WhoIsThatServer.Recognition.Controllers
{
    public interface IDatabaseController
    {
        /// <summary>
        /// Calls ImageObjectElementController in backend and returns list of image objects
        /// </summary>
        /// <returns>List of ImageObject instances</returns>
        Task<List<ImageModel>> GetImageObjects();
    }
}
