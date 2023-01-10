using Microsoft.AspNetCore.Mvc;
using Social.Application.Contracts;
using Social.Application.Dto;
using System.Net;

namespace Social.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IFollowService _followService;
        public UserController(IFollowService followService)
        {
            _followService = followService;
        }

        // for debugging
        [HttpPost]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateUser([FromBody] UserDto user)
        {
            var result = await _followService.CreateUserNode(user);

            if (result != null)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
