using System;
using MediatR;

namespace Application.Commands.AddSubscriberCommand
{
    public record AddSubscriberCommand(Guid SubscriberId, Guid ClientId):IRequest<Unit>;
}