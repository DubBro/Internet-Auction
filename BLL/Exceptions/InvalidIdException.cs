using System;

namespace BLL.Exceptions
{
    public class InvalidIdException : Exception
    {
        public InvalidIdException(string message = "ERROR: Invalid ID") : base(message)
        {

        }
    }
}
