#region

using System;
using MediatR;

#endregion

namespace Application.Queries.GetClient
{
    public record GetClientQuery(Guid ClientId) : IRequest<ClientDto?>;
}