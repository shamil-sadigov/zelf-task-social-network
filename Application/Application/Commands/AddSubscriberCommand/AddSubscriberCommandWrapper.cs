using Domain.ValueObjects;

namespace Application.Commands.AddSubscriberCommand
{
    internal record AddSubscriberCommandWrapper
    {
        public AddSubscriberCommandWrapper(AddSubscriberCommand command)
        {
            SubscriberId = new ClientId(command.SubscriberId);
            ClientId = new ClientId(command.ClientId);
        }

        internal ClientId SubscriberId { get; } 
        internal ClientId ClientId { get; } 
        
    }
}