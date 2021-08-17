#region

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using MediatR;

#endregion

namespace Application.Queries.GetTopPopularClients
{
    public class
        GetTopPopularClientsQueryHandler : IRequestHandler<GetTopPopularClientsQuery, IReadOnlyCollection<ClientDto>>
    {
        private readonly IClientQueryRepository _clientRepository;

        public GetTopPopularClientsQueryHandler(IClientQueryRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IReadOnlyCollection<ClientDto>> Handle(
            GetTopPopularClientsQuery request,
            CancellationToken cancellationToken)
        {
            RowLimit rowLimit = new();

            if (request.Limit.HasValue)
                rowLimit.ChangeLimit(request.Limit.Value);

            var topPopularClients = await _clientRepository.GetTopPopularAsync(rowLimit.Value);

            return topPopularClients;
        }
    }
}