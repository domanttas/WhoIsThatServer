using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhoIsThatServer.Recognition.ErrorMessages
{
    public static class RecognitionErrorMessages
    {
        public const string NoFacesFoundError = "No faces were detected!";
        public const string NoOneIdentifiedError = "No one was indetified!";
        public const string WrongUriError = "Photo was not successfully taken!";

        public const string PersonNotCreatedError = "Person was not successfully registered!";
    }
}
