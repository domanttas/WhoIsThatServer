﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.Context
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        /// <summary>
        /// Generates database called WhoIsThatServer.Storage.DatabaseContext
        /// Generates Tables according to their names from properties of type DbSet<T>.
        /// Used to access DB information.
        /// </summary>
        public DatabaseContext() : base("Data Source=teststorageserverdbserver.database.windows.net;Initial Catalog=TestStorageServer_db;User ID=sqladmin;Password=Vs8rTr3k;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
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