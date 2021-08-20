#region

using System.Linq;
using Domain.Contracts;
using Domain.ValueObjects;

#endregion

namespace Domain
{
    public class SubscriptionBasedPopularityEvaluator : IPopularityEvaluator
    {
        private readonly Client _client;

        public SubscriptionBasedPopularityEvaluator(Client client)
        {
            _client = client;
        }

        public ClientPopularity Evaluate()
        {
            var count = _client.Subscribers.Count(x => x.ClientId == _client.Id);

            return new ClientPopularity((uint) count);
        }
    }
}