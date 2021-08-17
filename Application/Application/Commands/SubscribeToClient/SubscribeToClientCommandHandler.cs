using System.Threading;
using System.Threading.Tasks;
using Application.Commands.CreateClient;
using Domain;
using Domain.Contracts;
using Domain.ValueObjects;
using MediatR;

namespace Application.Commands.SubscribeToClient
{
    public class SubscribeToClientCommandHandler:IRequestHandler<SubscribeToClientCommand>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IClientCounter _clientCounter;

        public SubscribeToClientCommandHandler(IClientRepository clientRepository, IClientCounter clientCounter)
        {
            _clientRepository = clientRepository;
            _clientCounter = clientCounter;
        }
        
        public async Task<Unit> Handle(SubscribeToClientCommand request, CancellationToken cancellationToken)
        {
            var command = new SubscribeToClientCommandWrapper(request);

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