using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhoIsThatServer.Recognition.Models;

namespace WhoIsThatServer.Recognition.Controllers
{
    public interface IRecognitionServicesController
    {
        /// <summary>
        /// Calls method responsible for image recognition in RecognitionServices
        /// </summary>
        /// <returns>Name of detected person</returns>
        Task<JsonResult> InitiateRecognition();

        /// <summary>
        /// Inserts person into person group
        /// </summary>
        /// <param name="imageModel">Object of person from frontend</param>
        /// <returns>Boolean</returns>
        Task<JsonResult> Post([FromBody] ImageModel imageModel);

        /// <summary>
        /// Creates person group
        /// </summary>
        /// <returns></returns>
        Task<JsonResult> Create();

        /// <summary>
        /// Detects features of face
        /// </summary>
        /// <param name="imageModel">Object of person from frontend</param>
        /// <returns>Detected features as array</returns>
        Task<JsonResult> DetectFeaturesOfFace([FromBody] ImageModel imageModel);
    }
}
