using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.Helpers
{
    public interface IFaceFeaturesHelper
    {
        /// <summary>
        /// Inserts new features model into DB
        /// </summary>
        /// <param name="personId">ID of person</param>
        /// <param name="age">Age returned by recognition API</param>
        /// <param name="gender">Gender returned by recognition API</param>
        /// <returns>Inserted model</returns>
        FaceFeaturesModel InsertNewFeaturesModel(int personId, int age, string gender);

        /// <summary>
        /// Gets feature model by person ID
        /// </summary>
        /// <param name="id">ID of person</param>
        /// <returns>Features model</returns>
        FaceFeaturesModel GetFaceFeaturesByPersonId(int id);
    }
}
