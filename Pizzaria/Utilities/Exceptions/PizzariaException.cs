using System.Net;
using System.Runtime.Serialization;

namespace Pizzaria.Utilities.Exceptions
{
    [Serializable]
    public class PizzariaException : Exception
    {
        private HttpStatusCode StatusCode { get; }
        private HttpContent Content { get; }

        public PizzariaException()
        {
        }

        public PizzariaException(string? message) : base(message)
        {
        }

        public PizzariaException(HttpStatusCode statusCode, HttpContent content)
        {
            this.StatusCode = statusCode;
            this.Content = content;
        }

        public PizzariaException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected PizzariaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
