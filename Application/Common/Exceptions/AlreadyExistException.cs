using System;
using System.Collections.Generic;

namespace Application.Common.Exceptions
{
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException()
            : base()
        {
        }

        public AlreadyExistException(string message)
            : base(message)
        {
        }

        public AlreadyExistException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public AlreadyExistException(string name, string field, long key, List<long> ids
        )
            : base($"Entity '{name}' {field}:({key}) was found. ids : ({string.Join(", ", ids)})")
        {
        }
    }
}