using System;
using MediatR;

namespace Application.Commands.CreateClient
{
    public record CreateClientCommand(string ClientName):IRequest<Guid>;
}