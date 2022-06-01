using System;

namespace BLL.Exceptions
{
    public class InvalidCategoryException : Exception
    {
        public InvalidCategoryException(string message = "ERROR: Invalid category") : base(message)
        {

        }
    }
}
