#region

using System;
using System.Collections.Generic;
using System.Linq;
using Domain.BuildingBlocks.BuildingBlocks;
using Domain.Contracts;
using Domain.DomainEvents;
using Domain.Exceptions;
using Domain.ValueObjects;

#endregion

namespace Domain
{
    public class Client:Entity, IAggregateRoot
    {
        private readonly IPopularityEvaluator _popularityEvaluator;
        
        private readonly List<Subscriber> _subscribers = new();
        
        private ClientName _clientName;
        private ClientPopularity _clientPopularity;

        // For EF
        private Client()
        {
            _popularityEvaluator = new SubscriptionBasedPopularityEvaluator(this);
        }

        private Client(ClientName clientName) 
            : this()
        {
            _clientName = clientName;
            Id = new ClientId(Guid.NewGuid());
            
            AddDomainEvent(new ClientCreatedDomainEvent(Id, _clientName));
        }
        
        internal IReadOnlyList<Subscriber> Subscribers => _subscribers;
        
        public ClientId Id { get; }

        public static Client WithName(ClientName name, IClientCounter clientCounter)
        {
            var numberOfExistingClients = clientCounter.CountByName(name);

            if (numberOfExistingClients > 0)
                throw new DuplicateClientNameException(name);

            return new Client(name);
        }
        
        public void AddSubscriber(Client subscriber)
        {
            SubscriberMustBeUnique(subscriber.Id);

            AddSubscriberInternal(subscriber);

            EvaluatePopularity();
            
            AddDomainEvent(new ClientSubscribedDomainEvent(subscriber.Id, Id, _clientPopularity));
        }

        private void EvaluatePopularity() 
            => _clientPopularity = _popularityEvaluator.Evaluate();

        private void AddSubscriberInternal(Client subscriber)
        {
            var subscription = Subscriber.Builder
                .Subscribe(subscriber.Id)
                .To(clientId: Id);

            _subscribers.Add(subscription);
        }

        private void SubscriberMustBeUnique(ClientId subscriber)
        {
            var subscriptionExists = _subscribers
                .Any(x => x.SubscriberId == subscriber
                          && x.ClientId == Id);

            if (subscriptionExists) 
                throw new DuplicateSubscriberException(subscriber, Id);
        }
    }
}