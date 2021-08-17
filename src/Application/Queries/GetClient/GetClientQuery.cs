#region

using System;
using System.Collections.Generic;
using Domain.DomainEvents;
using MediatR;

#endregion

namespace Application.Queries.GetClient
{
    public record GetClientQuery(Guid ClientId) : IRequest<ClientDto?>;
}