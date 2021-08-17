using System.Collections.Generic;
using MediatR;

namespace Application.Queries.GetTopPopularClients
{
    public record GetTopPopularClientsQuery(ushort? Limit) : IRequest<IReadOnlyCollection<ClientDto>>;
}