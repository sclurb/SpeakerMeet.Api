using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Core.Interfaces.Logging;
using SpeakerMeet.Core.Interfaces.Services;

namespace SpeakerMeet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunitiesController : ControllerBase
    {
        private readonly ICommunityService _communityService;
        private readonly ILoggerAdapter<CommunitiesController> _logger;

        public CommunitiesController(
            ICommunityService communityService,
            ILoggerAdapter<CommunitiesController> logger
        )
        {
            _logger = logger;
            _communityService = communityService;
        }

        // GET: api/Communities
        [HttpGet]
        [ProducesResponseType(typeof(CommunitiesResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAll(int pageIndex = 0, int itemsPage = 100, string? direction = null)
        {
            try
            {
                var result = await _communityService.GetAll(pageIndex, itemsPage, direction);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Communities");
        }

        // GET: api/Communities/5
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(CommunityResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var result = await _communityService.Get(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Community");
        }

        // GET: api/Communities/slug-name
        [HttpGet("{slug}")]
        [ProducesResponseType(typeof(CommunityResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            try
            {
                var result = await _communityService.Get(slug);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Community");
        }

        // GET: api/Communities/Featured
        [HttpGet("Featured")]
        [ProducesResponseType(typeof(CommunitiesResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetFeatured()
        {
            try
            {
                var result = await _communityService.GetFeatured();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Communities");
        }

        // POST: api/Communities
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PostAsync([FromBody] CommunityAdd communityAdd)
        {
            var result = new Community();
            if(communityAdd == null)
            {
                return BadRequest();
            }
            else
            {
               result = await _communityService.CreateCommunity(communityAdd);
            }

            return Ok(result);
        }

        // PUT: api/Communities/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put([FromBody] CommunityUpdate communityUpdate)
        {
            if(communityUpdate == null)
            {
                return BadRequest();
            }
            else
            {
                await _communityService.UpdateCommunity(communityUpdate);
                return NoContent();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _communityService.DeleteCommunity(id);

            if(result > 0)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}/tags")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateTags(Guid id, string[] tags)
        {
            try
            {
                var result = await _communityService.UpdateTags(id, tags);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Community");
        }
    }
}
