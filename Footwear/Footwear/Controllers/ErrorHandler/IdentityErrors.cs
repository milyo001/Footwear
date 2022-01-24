
namespace Footwear.Controllers.ErrorHandler
{
    public static class IdentityErrors
    {
        //User Error Constants
        public const string InvalidData = "Invalid input data!";
        public const string UserIsInUse = "User already exists!";
        public const string CannotRegister = "Unable to register! Please contact administrator!";
        public const string InvalidUsernamePassword = "Username or password is incorrect.";
        public const string UnableToGetUserInfo = "Unable to get user information. Please contact administrator!";
        public const string UnableToUpdateUserInfo = "Unable to update user information. Please contact administrator!";
        public const string EmailInUse = "Email already in use.";
        public const string UnableToUpdateEmail = "Unable to update user email. Please contact administrator!";
        public const string PasswordsNotMatch = "New passwords does not match!";
        public const string UnableToUpdatePassword = "Unable to update user password. Please contact administrator!";
        public const string InvalidPassword = "Unable to change password. Password is invalid!";
    }
}
