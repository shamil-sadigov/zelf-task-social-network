#region

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using MediatR;

#endregion

namespace Application.Queries.GetClientSubscribers
{
    public class GetClientSubscribersQueryHandler : IRequestHandler<GetClientSubscribersQuery, List<ClientDto>>
    {
        private readonly IClientQueryRepository _clientRepository;

        public GetClientSubscribersQueryHandler(IClientQueryRepository clientRepository) 
            => _clientRepository = clientRepository;

        public async Task<List<ClientDto>> Handle(GetClientSubscribersQuery request, CancellationToken cancellationToken)
        {
            var result = await _clientRepository.GetClientSubscribersAsync(request.ClientId);
            
            return result;
        }
    }
}