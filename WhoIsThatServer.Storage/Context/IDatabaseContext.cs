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
        DbSet<DatabaseImageElement> DatabaseImageElements { get; set; }
    }
}