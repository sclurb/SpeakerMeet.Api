﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Interfaces.Logging;
using SpeakerMeet.Core.Interfaces.Services;

namespace SpeakerMeet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConferencesController : ControllerBase
    {
        private readonly IConferenceService _conferenceService;
        private readonly ILoggerAdapter<ConferencesController> _logger;

        public ConferencesController(
            IConferenceService conferenceService,
            ILoggerAdapter<ConferencesController> logger
        )
        {
            _logger = logger;
            _conferenceService = conferenceService;
        }

        // GET: api/Conferences
        [HttpGet]
        [ProducesResponseType(typeof(ConferencesResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAll(int pageIndex = 0, int itemsPage = 100, string? direction = null)
        {
            try
            {
                var result = await _conferenceService.GetAll(pageIndex, itemsPage, direction);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Conferences");
        }

        // GET: api/Conferences/5
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(ConferenceResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var result = await _conferenceService.Get(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Conference");
        }

        // GET: api/Conferences/slug-name
        [HttpGet("{slug}")]
        [ProducesResponseType(typeof(ConferenceResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            try
            {
                var result = await _conferenceService.Get(slug);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Conference");
        }

        // GET: api/Conferences/Featured
        [HttpGet("Featured")]
        [ProducesResponseType(typeof(ConferenceFeatured), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetFeatured()
        {
            try
            {
                var result = await _conferenceService.GetFeatured();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest("Unable to return Conferences");
        }

        // POST: api/Conferences
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Conferences/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
