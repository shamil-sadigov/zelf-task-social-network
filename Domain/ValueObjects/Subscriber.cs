using Domain.BuildingBlocks.BuildingBlocks;

namespace Domain.ValueObjects
{
    public class Subscriber:Entity
    {
        // For EF
        private Subscriber()
        {
            
        }
        
        private Subscriber(ClientId subscriberId, ClientId clientId)
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

            public Subscriber To(ClientId clientId)
                => new(_subscriber, clientId);
        }
    }
}