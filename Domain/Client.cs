#region

using System;
using System.Collections.Generic;
using System.Linq;
using Domain.BuildingBlocks;
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
        
        private readonly List<ClientSubscriber> _subscribers = new();
        
        private ClientName _name;
        private ClientPopularity _popularity;

        // For EF
        private Client()
        {
            _popularityEvaluator = new SubscriptionBasedPopularityEvaluator(this);
        }

        private Client(ClientName name) 
            : this()
        {
            _name = name;
            Id = new ClientId(Guid.NewGuid());
            
            AddDomainEvent(new ClientCreatedDomainEvent(Id, _name));
        }
        
        internal IReadOnlyList<ClientSubscriber> Subscribers => _subscribers;
        
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
            
            AddDomainEvent(new ClientSubscribedDomainEvent(subscriber.Id, Id, _popularity));
        }

        private void EvaluatePopularity() 
            => _popularity = _popularityEvaluator.Evaluate();

        private void AddSubscriberInternal(Client subscriber)
        {
            var subscription = ClientSubscriber.Builder
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