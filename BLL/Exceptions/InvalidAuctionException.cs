using System;

namespace BLL.Exceptions
{
    public class InvalidAuctionException : Exception
    {
        public InvalidAuctionException(string message = "ERROR: Invalid auction") : base(message)
        {

        }
    }
}
