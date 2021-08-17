#region

using System;
using MediatR;

#endregion

namespace Application.Commands.AddSubscriberCommand
{
    public record AddClientSubscriberCommand(Guid SubscriberId, Guid ClientId) : ICommand<Unit>;
}