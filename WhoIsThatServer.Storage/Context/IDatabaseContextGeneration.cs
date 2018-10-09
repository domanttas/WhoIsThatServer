using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhoIsThatServer.Storage.Context
{
    public interface IDatabaseContextGeneration
    {
        DatabaseContext BuildDatabaseContext();
    }
}