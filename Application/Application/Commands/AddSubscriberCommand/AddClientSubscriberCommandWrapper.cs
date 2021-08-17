#region

using Domain.ValueObjects;

#endregion

namespace Application.Commands.AddSubscriberCommand
{
    internal record AddClientSubscriberCommandWrapper
    {
        public AddClientSubscriberCommandWrapper(AddClientSubscriberCommand command)
        {
            SubscriberId = new ClientId(command.SubscriberId);
            ClientId = new ClientId(command.ClientId);
        }

        internal ClientId SubscriberId { get; }
        internal ClientId ClientId { get; }
    }
}