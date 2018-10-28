using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhoIsThatServer.Recognition.Exceptions
{
    public class ManagerException : Exception
    {
        public string ErrorCode { get; set; }

        public ManagerException() : base()
        {
        }

        public ManagerException(string code) : base()
        {
            ErrorCode = code;
        }
    }
}
