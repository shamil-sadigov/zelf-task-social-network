#region

using System;
using Domain.ValueObjects;

#endregion

namespace Domain.Exceptions
{
    public class DuplicateSubscriberException : ApplicationException
    {
        public DuplicateSubscriberException(ClientId subscriber, ClientId clientId)
            : base($"Client '{subscriber}' has been already subscribed to '{clientId}'")
        {
            Subscriber = subscriber;
            ClientId = clientId;
        }

        public ClientId Subscriber { get; }
        public ClientId ClientId { get; }
    }
}