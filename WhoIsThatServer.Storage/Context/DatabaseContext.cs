using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WhoIsThatServer.Storage.Models;

namespace WhoIsThatServer.Storage.Context
{
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// Generates database called WhoIsThatServer.Storage.DatabaseContext
        /// Generates Tables according to their names from properties of type DbSet<T>.
        /// Used to access DB information.
        /// </summary>
        public DatabaseContext() : base()
        {

        }

        /// <summary>
        /// Stores information about database image element
        /// </summary>
        public virtual DbSet<DatabaseImageElement> DatabaseImageElement { get; set; }


    }
}