using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class GameOverException : Exception
    {
        public GameOverException()
        {
        }

        public GameOverException(string? message) : base(message)
        {
        }

        public GameOverException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
