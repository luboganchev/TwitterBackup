namespace TwitterBackup.Services.Exceptions
{
    using System.Collections.Generic;

    public class UserException : BaseTwitterBackupException<UserExceptionType>
    {
        public UserException(UserExceptionType type)
            : base(type)
        {
        }

        public UserException(UserExceptionType type, string message)
            : base(type, message)
        {
        }

        public UserException(UserExceptionType type, string message, Dictionary<string, string> parameters)
            : base(type, message, parameters)
        {
        }
    }
}
