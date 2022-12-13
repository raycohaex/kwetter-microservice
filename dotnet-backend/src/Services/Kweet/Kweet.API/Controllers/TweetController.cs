using Kweet.Application.Features.Queries.GetKweet;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MediatR;
using Kweet.Domain.Entities;
using Kweet.Application.Features.Commands.PostKweet;
using Eventbus.Messages.Events;
using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Kweet.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TweetController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publish;

        public TweetController(IMediator mediator, IMapper mapper, IPublishEndpoint publish)
        {
            _mapper = mapper;
            _mediator = mediator;
            _publish = publish;
        }

        [HttpGet("{tweetId}")]
        [ProducesResponseType(typeof(KweetViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<KweetViewModel>> GetKweetById(Guid tweetId)
        {
            var query = new GetKweetQuery(tweetId);
            var kweet = await _mediator.Send(query);
            return Ok(kweet);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Guid>> PostKweet([FromBody] PostKweetCommand command)
        {

            var user = User;

            // Get the username from the user's claims
            var usernameClaim = user.FindFirst("preferred_username");

            command.UserName = usernameClaim.Value;

            var result = await _mediator.Send(command);

            // The post command returns an entity,
            // I map it to an event and send it to MQ
            var eventMessage = _mapper.Map<KweetPostedEvent>(result);
            await _publish.Publish(eventMessage);

            return Ok(result);
        }
    }
}
