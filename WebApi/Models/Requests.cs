using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public record CreateClientRequest(string ClientName);
    
    public record AddClientSubscriberRequest(Guid SubscriberId);
}