using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.Context
{
    public interface IDatabaseContext
    {
        /// <summary>
        /// Stores information about database image element
        /// </summary>
        DbSet<DatabaseImageElement> DatabaseImageElements { get; set; }

        /// <summary>
        /// Stores information about target and hunter
        /// </summary>
        DbSet<TargetElement> TargetElements { get; set; }

        /// <summary>
        /// Stores information about one's face features
        /// </summary>
        DbSet<FaceFeaturesModel> FaceFeatures { get; set; }
    }
}