using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Application.Queries.GetClient
{
    public class GetClientQueryHandler:IRequestHandler<GetClientQuery, ClientDto?>
    {
        private readonly IClientQueryRepository _clientRepository;

        public GetClientQueryHandler(IClientQueryRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        
        public async Task<ClientDto?> Handle(GetClientQuery request, CancellationToken cancellationToken)
        {
            ClientDto? result = await _clientRepository.GetByIdAsync(request.ClientId);
            
            return result;
        }
    }
}