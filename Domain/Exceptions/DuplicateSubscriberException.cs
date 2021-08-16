using System;
using Domain.ValueObjects;

namespace Domain.Exceptions
{
    public class DuplicateSubscriberException:ApplicationException
    {
        public ClientId Subscriber { get; }
        public ClientId ClientId { get; }

        public DuplicateSubscriberException(ClientId subscriber, ClientId clientId)
            :base($"Client '{subscriber}' has been already subscribed to '{clientId}'")
        {
            Subscriber = subscriber;
            ClientId = clientId;
        }
    }
}