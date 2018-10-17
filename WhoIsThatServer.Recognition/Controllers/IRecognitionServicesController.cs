using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhoIsThatServer.Recognition.Controllers
{
    public interface IRecognitionServicesController
    {
        /// <summary>
        /// Calls method responsible for image recognition in RecognitionServices
        /// </summary>
        /// <returns>Name of detected person</returns>
        Task<JsonResult> InitiateRecognition();
    }
}
