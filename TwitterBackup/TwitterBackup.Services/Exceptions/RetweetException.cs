namespace TwitterBackup.Services.Exceptions
{
    using System.Collections.Generic;

    public class RetweetException : BaseTwitterBackupException<RetweetExceptionType>
    {
        public RetweetException(RetweetExceptionType type)
            : base(type)
        {
        }

        public RetweetException(RetweetExceptionType type, string message)
            : base(type, message)
        {
        }

        public RetweetException(RetweetExceptionType type, string message, Dictionary<string, string> parameters)
            : base(type, message, parameters)
        {
        }
    }
}
