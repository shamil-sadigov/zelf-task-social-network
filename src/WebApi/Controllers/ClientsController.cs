#region

using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Commands.AddSubscriberCommand;
using Application.Commands.CreateClient;
using Application.Queries;
using Application.Queries.GetClient;
using Application.Queries.GetClientSubscribers;
using Application.Queries.GetTopPopularClients;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

#endregion

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.Created, Type = typeof(ClientResponse))]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.Conflict)]
        public async Task<IActionResult> CreateClientAsync([FromBody] CreateClientRequest request)
        {
            var createdClientId = await _mediator.Send(new CreateClientCommand(request.ClientName));

            var result = await _mediator.Send(new GetClientQuery(createdClientId));

            var response = MapToResponse(result!);

            return CreatedAtAction(
                "GetClient",
                new {id = createdClientId},
                response);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(ClientResponse))]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult<ClientResponse>> GetClientAsync(Guid id)
        {
            var result = await _mediator.Send(new GetClientQuery(id));

            if (result is null)
                return NotFound();

            var response = MapToResponse(result);

            return response;
        }

        [HttpPost("{id:guid}/subscribers")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.Conflict)]
        public async Task<IActionResult> AddClientSubscriberAsync(
            Guid id,
            [FromBody] AddClientSubscriberRequest request)
        {
            await _mediator.Send(new AddClientSubscriberCommand
            (
                request.SubscriberId,
                ClientId: id
            ));

            return Ok();
        }
        
        [HttpGet("{id:guid}/subscribers")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<ClientsResponse> GetClientSubscribersAsync(Guid id)
        {
            var result = await _mediator.Send(new GetClientSubscribersQuery(id));

            if (result is null)
                return new ClientsResponse(Enumerable.Empty<ClientResponse>());

            var clients = result.Select(x => MapToResponse(x));

            return new ClientsResponse(clients);
        }
        
        [HttpGet("top-popular/{limit:int?}")]
        [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(ClientsResponse))]
        public async Task<ClientsResponse> GetTopPopularClientsAsync(ushort? limit = null)
        {
            var result = await _mediator.Send(new GetTopPopularClientsQuery(limit));

            if (result is null)
                return new ClientsResponse(Enumerable.Empty<ClientResponse>());
            
            var clients = result.Select(x => MapToResponse(x));

            var response = new ClientsResponse(clients);

            return response;
        }
        
        private static ClientResponse MapToResponse(ClientDto dto)
            => new(dto.Id, dto.Name, dto.Popularity);
    }
}