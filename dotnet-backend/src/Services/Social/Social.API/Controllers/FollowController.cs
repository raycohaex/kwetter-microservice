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

        [HttpPost]
        [ProducesResponseType(typeof(RelationDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateRelationship([FromBody] RelationDto relation)
        {
            await _followService.CreateFollowRelation(relation);

            return Ok();
        }
    }
}
