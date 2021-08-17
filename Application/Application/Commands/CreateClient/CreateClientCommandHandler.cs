using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.Contracts;
using Domain.ValueObjects;
using MediatR;

namespace Application.Commands.CreateClient
{
    public class CreateClientCommandHandler:IRequestHandler<CreateClientCommand, Guid>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IClientCounter _clientCounter;

        public CreateClientCommandHandler(IClientRepository clientRepository, IClientCounter clientCounter)
        {
            _clientRepository = clientRepository;
            _clientCounter = clientCounter;
        }
        
        public async Task<Guid> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var clientName = new ClientName(request.ClientName);

            var createdClient = await Client.CreateWithNameAsync(clientName, _clientCounter);

            await _clientRepository.AddAsync(createdClient);
            
            return createdClient.Id;
        }
    }
}