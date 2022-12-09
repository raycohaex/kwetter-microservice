using Microsoft.AspNetCore.Mvc;
using Social.Application.Contracts;
using Social.Application.Dto;
using System.Net;

namespace Social.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FollowController : ControllerBase
    {
        private readonly IFollowService _followService;
        public FollowController(IFollowService followService)
        {
            _followService = followService;
        }

        [HttpPost("{tweetId}")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] UserDto user)
        {
            var result = _followService.CreateUserNode(user);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}
