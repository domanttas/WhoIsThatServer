using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhoIsThatServer.Storage.Context
{
    public class DatabaseContextGeneration : IDatabaseContextGeneration
    {
        public DatabaseContext BuildDatabaseContext()
        {
            return new DatabaseContext();
        }
    }
}