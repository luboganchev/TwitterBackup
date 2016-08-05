namespace TwitterBackup.Services.Exceptions
{
    using System;
    using System.Collections.Generic;

    public class TweetException : Exception
    {
        private TweetExceptionType typeOfException;
        private Dictionary<string, string> parameters;

        public TweetException(TweetExceptionType type)
        {
            this.typeOfException = type;
        }

        public TweetException(TweetExceptionType type, string message)
            : base(message)
        {
            this.typeOfException = type;
        }

        public TweetException(TweetExceptionType type, string message, Dictionary<string, string> parameters)
            : base(message)
        {
            this.typeOfException = type;
            this.parameters = parameters;
        }

        public TweetExceptionType Type
        {
            get
            {
                return this.typeOfException;
            }
        }

        public Dictionary<string, string> Parameters
        {
            get
            {
                return this.parameters;
            }
        }
    }
}
