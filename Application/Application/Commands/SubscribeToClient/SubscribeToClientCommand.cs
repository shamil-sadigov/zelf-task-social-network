using System;
using MediatR;

namespace Application.Commands.SubscribeToClient
{
    public record SubscribeToClientCommand(Guid SubscriberId, Guid ClientId):IRequest<Unit>;
}