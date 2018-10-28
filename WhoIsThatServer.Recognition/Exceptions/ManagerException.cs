using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhoIsThatServer.Recognition.ErrorMessages;

namespace WhoIsThatServer.Recognition.Exceptions
{
    public class ManagerException : Exception
    {
        public string ErrorCode;

        public ManagerException() : base()
        {
        }

        public ManagerException(string code) : base()
        {
            ErrorCode = code;
        }
    }
}
