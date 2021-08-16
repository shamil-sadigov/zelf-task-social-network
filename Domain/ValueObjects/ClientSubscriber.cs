using Domain.BuildingBlocks;

namespace Domain.ValueObjects
{
    public class ClientSubscriber:Entity
    {
        // For EF
        private ClientSubscriber()
        {
            
        }
        
        private ClientSubscriber(ClientId subscriberId, ClientId clientId)
        {
            SubscriberId = subscriberId;
            ClientId = clientId;
        }

        public ClientId SubscriberId { get; }

        public ClientId ClientId { get; }
        
        public class Builder
        {
            private readonly ClientId _subscriber;

            private Builder(ClientId subscriber)
                => _subscriber = subscriber;

            public static Builder Subscribe(ClientId subscriber)
                => new(subscriber);

            public ClientSubscriber To(ClientId clientId)
                => new(_subscriber, clientId);
        }
    }
}