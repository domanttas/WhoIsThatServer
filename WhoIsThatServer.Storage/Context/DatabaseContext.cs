using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.Azure;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.Context
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        /// <inheritdoc />
        public DatabaseContext() : base(CloudConfigurationManager.GetSetting("DbConnectionString"))
        {

        }

        /// <inheritdoc/>
        public virtual DbSet<DatabaseImageElement> DatabaseImageElements { get; set; }

        /// <inheritdoc/>
        public virtual DbSet<TargetElement> TargetElements { get; set; }

        /// <inheritdoc/>
        public virtual DbSet<FaceFeaturesModel> FaceFeatures { get; set; }
    }
}