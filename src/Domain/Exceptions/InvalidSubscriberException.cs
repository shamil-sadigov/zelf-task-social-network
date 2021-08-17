using Domain.ValueObjects;

namespace Domain.Exceptions
{
    public class InvalidSubscriberException : DomainExceptionBase
    {
        public InvalidSubscriberException(ClientId clientId, string message)
            : base(message)
        {
            ClientId = clientId;
        }

        public ClientId ClientId { get; }
    }
}