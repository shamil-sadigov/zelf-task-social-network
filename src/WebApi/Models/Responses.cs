#region

using System;
using System.Collections.Generic;

#endregion

namespace WebApi.Models
{
    public record ClientsResponse(IReadOnlyCollection<ClientResponse> Items);

    public record ClientResponse(Guid Id, string Name, uint Popularity);
}