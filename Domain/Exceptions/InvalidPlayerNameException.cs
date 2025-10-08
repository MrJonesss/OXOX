using System.Xml.Linq;

namespace Domain.Exceptions
{
    [Serializable]
    public class InvalidPlayerNameException : Exception
    {
        public InvalidPlayerNameException()
        {
        }

        public InvalidPlayerNameException(string? name) 
            : base($"Ongeldige spelernaam: '{name ?? "null"}'. Naam mag niet leeg of spaties zijn.")
        {
        }

        public InvalidPlayerNameException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}