using System;
using System.Collections.Generic;
using MediatR;

namespace Application.Queries.GetClient
{
    public record GetClientQuery(Guid ClientId) : IRequest<ClientDto?>;
}