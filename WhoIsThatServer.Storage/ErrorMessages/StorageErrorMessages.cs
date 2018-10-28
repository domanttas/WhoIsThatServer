namespace WhoIsThatServer.Storage.ErrorMessages
{
    public static class StorageErrorMessages
    {
        public const string TargetNotFoundError = "This is not your target";

        public const string UserDoesNotExistError = "User with ID was not found";

        public const string InvalidImageUriError = "Invalid URI of image";

        public const string InvalidFileNameError = "Image filename should be alphanumeric, may include dashes and underscores";
    }
}