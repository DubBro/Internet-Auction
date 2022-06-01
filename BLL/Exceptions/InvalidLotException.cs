using System;

namespace BLL.Exceptions
{
    public class InvalidLotException : Exception
    {
        public InvalidLotException(string message = "ERROR: Invalid lot") : base(message)
        {

        }
    }
}
