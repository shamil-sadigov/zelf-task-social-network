#region

using System;
using System.Collections.Generic;
using MediatR;

#endregion

namespace Application.Queries.GetClientSubscribers
{
    public record GetClientSubscribersQuery(Guid ClientId) : IRequest<List<ClientDto>>;
}