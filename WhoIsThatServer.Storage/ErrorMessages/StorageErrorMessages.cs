namespace WhoIsThatServer.Storage.ErrorMessages
{
    public static class StorageErrorMessages
    {
        public const string TargetNotFoundError = "This is not your target";

        public const string UserDoesNotExistError = "User with ID was not found";

        public const string InvalidImageUriError = "Invalid URI of image";

        public const string InvalidFileNameError = "Image filename should be alphanumeric, may include dashes and underscores";

        public const string TargetAlreadyAssignedError = "You already have target assigned";

        public const string TargetNotAssignedError = "Target was not assigned";

        public const string ThereAreNoPlayersError = "There are no other players";

        public const string TargetNotPresentAtLaunchError = "Target not found";
    }
}