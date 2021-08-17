using System;
using MediatR;

namespace Application.Commands.AddSubscriberCommand
{
    public record AddClientSubscriberCommand(Guid SubscriberId, Guid ClientId):IRequest<Unit>;
}