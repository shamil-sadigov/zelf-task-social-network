#region

using System;

#endregion

namespace WebApi.Models
{
    public record CreateClientRequest(string ClientName);

    public record AddClientSubscriberRequest(Guid SubscriberId);
}