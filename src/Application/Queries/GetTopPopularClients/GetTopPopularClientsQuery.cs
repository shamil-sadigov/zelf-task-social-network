#region

using System.Collections.Generic;
using MediatR;

#endregion

namespace Application.Queries.GetTopPopularClients
{
    public record GetTopPopularClientsQuery(ushort? Limit) : IRequest<IReadOnlyCollection<ClientDto>?>;
}