namespace TwitterBackup.Services.Exceptions
{
    using System;
    using System.Collections.Generic;

    public class BaseTwitterBackupException<T>: Exception
        where T : struct, IConvertible
    {
        private T typeOfException;
        private Dictionary<string, string> parameters;

        public BaseTwitterBackupException(T type)
        {
            this.typeOfException = type;
        }

        public BaseTwitterBackupException(T type, string message)
            : base(message)
        {
            this.typeOfException = type;
        }

        public BaseTwitterBackupException(T type, string message, Dictionary<string, string> parameters)
            : base(message)
        {
            this.typeOfException = type;
            this.parameters = parameters;
        }

        public T Type
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
