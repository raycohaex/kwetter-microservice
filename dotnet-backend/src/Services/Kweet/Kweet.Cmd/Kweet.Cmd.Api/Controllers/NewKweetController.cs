using AutoMapper;
using Kweet.Application.Features.Commands.PostKweet;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Kweet.Cmd.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NewKweetController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

            public NewKweetController(IMediator mediator, IMapper mapper)
            {
                _mapper = mapper;
                _mediator = mediator;
            }

            [HttpPost]
            [ProducesResponseType((int)HttpStatusCode.OK)]
            public async Task<ActionResult<Guid>> PostKweet([FromBody] PostKweetCommand command)
            {
                command.Id = new Guid();
                await _mediator.Send(command);

                return Ok(command.Id);
            }
    }
}