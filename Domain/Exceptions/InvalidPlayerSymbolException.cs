namespace Domain.Exceptions
{
    [Serializable]
    public class InvalidPlayerSymbolException : Exception
    {
        private char symbol;

        public InvalidPlayerSymbolException()
        {
        }

        public InvalidPlayerSymbolException(char symbol) 
            : base($"Ongeldig speler symbool: '{symbol}'. Symbool moet 'X' of 'O' zijn.")
        {
        }

        public InvalidPlayerSymbolException(string? message) : base(message)
        {
        }

        public InvalidPlayerSymbolException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}