using System;

namespace WhoIsThatServer.Storage.Exceptions
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