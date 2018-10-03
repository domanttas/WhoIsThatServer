using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace WhoIsThatServer.Storage.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<WhoIsThatServer.Storage.Context.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "WhoIsThatServer.Storage.Context.DatabaseContext";
        }
    }
}