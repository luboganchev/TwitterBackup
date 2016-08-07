namespace TwitterBackup.Services.Exceptions
{
    using System;
    using System.Collections.Generic;

    public class TweetException : BaseTwitterBackupException<TweetExceptionType>
    {
        public TweetException(TweetExceptionType type)
            : base(type)
        {
        }

        public TweetException(TweetExceptionType type, string message)
            : base(type, message)
        {
        }

        public TweetException(TweetExceptionType type, string message, Dictionary<string, string> parameters)
            : base(type, message, parameters)
        {
        }
    }
}
