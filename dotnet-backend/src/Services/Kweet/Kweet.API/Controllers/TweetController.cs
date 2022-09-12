using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Kweet.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TweetController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TweetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{tweetId}", Name = "GetTweet")]
        [ProducesResponseType(typeof(IEnumerable<OrdersVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrdersVm>>> GetOrdersByUserName(string userName)
        {
            var query = new GetOrdersListQuery(userName);
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }
    }
}
