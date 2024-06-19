using System.Globalization;

namespace DollarInfo.Utils.Exceptions
{
    public class AuthException : Exception
    {
        public AuthException() : base("Authentication failed! Try again.") { }

        public AuthException(string message) : base(message) { }

        public AuthException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}