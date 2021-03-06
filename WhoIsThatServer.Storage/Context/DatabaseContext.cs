﻿using System;
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
        public DatabaseContext() : base("Data Source = teststorageserverdbserver.database.windows.net; Initial Catalog = TestStorageServer_db; User ID = sqladmin; Password=Vs8rTr3k;Connect Timeout = 30; Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        {

        }

        /// <inheritdoc/>
        public virtual DbSet<DatabaseImageElement> DatabaseImageElements { get; set; }

        /// <inheritdoc/>
        public virtual DbSet<TargetElement> TargetElements { get; set; }

        /// <inheritdoc/>
        public virtual DbSet<FaceFeaturesModel> FaceFeatures { get; set; }
        
        /// <inheritdoc/>
        public virtual DbSet<HistoryModel> History { get; set; }
    }
}