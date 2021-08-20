#region

using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.Contracts;
using Domain.ValueObjects;
using MediatR;

#endregion

namespace Application.Commands.CreateClient
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Guid>
    {
        private readonly IClientRepository _clientRepository;

        public CreateClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Guid> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var clientName = new ClientName(request.ClientName);

            var createdClient = Client.CreateWithName(clientName);

            await _clientRepository.AddAsync(createdClient);

            return createdClient.Id;
        }
    }
}