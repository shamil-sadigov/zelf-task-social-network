using Domain.ValueObjects;

namespace Application.Commands.SubscribeToClient
{
    internal record SubscribeToClientCommandWrapper
    {
        public SubscribeToClientCommandWrapper(SubscribeToClientCommand command)
        {
            SubscriberId = new ClientId(command.SubscriberId);
            ClientId = new ClientId(command.ClientId);
        }

        internal ClientId SubscriberId { get; } 
        internal ClientId ClientId { get; } 
        
    }
}