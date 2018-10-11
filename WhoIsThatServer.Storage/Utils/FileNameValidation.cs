using System.Text.RegularExpressions;

namespace WhoIsThatServer.Storage.Utils
{
    public static class FileNameValidation
    {
        public static bool IsFileNameValid(this string fileName)
        {
            var regex = new Regex(@"^[\w\-. ]+$");
            return regex.IsMatch(fileName);
        }
    }
}