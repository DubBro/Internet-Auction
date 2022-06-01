using System;

namespace BLL.Exceptions
{
    public class InvalidNameException : Exception
    {
        public InvalidNameException(string message = "ERROR: Invalid name") : base(message)
        {

        }
    }
}
