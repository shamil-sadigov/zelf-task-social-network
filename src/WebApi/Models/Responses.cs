#region

using System;
using System.Collections.Generic;

#endregion

namespace WebApi.Models
{
    public record TopPopularClientsResponse(IReadOnlyCollection<ClientResponse> Items);

    public record ClientResponse(Guid Id, string Name, ushort Popularity);
}