#region

using System;
using MediatR;

#endregion

namespace Application.Commands.CreateClient
{
    public record CreateClientCommand(string ClientName) : IRequest<Guid>;
}