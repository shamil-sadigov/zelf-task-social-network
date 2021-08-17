#region

using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.Contracts;
using Domain.ValueObjects;
using MediatR;

#endregion

namespace Application.Commands.AddSubscriberCommand
{
    public class AddClientSubscriberCommandHandler : IRequestHandler<AddClientSubscriberCommand>
    {
        private readonly IClientRepository _clientRepository;

        public AddClientSubscriberCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Unit> Handle(AddClientSubscriberCommand request, CancellationToken cancellationToken)
        {
            var command = new AddClientSubscriberCommandWrapper(request);

            var client = await FindClientAsync(command.ClientId);
            var subscriber = await FindClientAsync(command.SubscriberId);

            client.AddSubscriber(subscriber);

            _clientRepository.Update(client);

            return Unit.Value;
        }

        private async Task<Client> FindClientAsync(ClientId id)
        {
            var client = await _clientRepository.GetAsync(id);

            if (client is null)
                throw new EntityNotFoundException<ClientId>(id, nameof(Client));

            return client;
        }
    }
}