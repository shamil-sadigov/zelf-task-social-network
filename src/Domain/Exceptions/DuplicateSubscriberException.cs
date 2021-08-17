#region

using System.Drawing;
using Domain.ValueObjects;

#endregion

namespace Domain.Exceptions
{
    public class DuplicateSubscriberException : DomainExceptionBase
    {
        public DuplicateSubscriberException(ClientId subscriberId, ClientId clientId)
            : base($"Client '{subscriberId}' has been already subscribed to '{clientId}'")
        {
            SubscriberId = subscriberId;
            ClientId = clientId;
        }

        public ClientId SubscriberId { get; }
        public ClientId ClientId { get; }
    }
}