using Application;
using Application.Guest.Commands;
using Application.Guest.DTOs;
using Application.Guest.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuestController : ControllerBase
    {
        private readonly ILogger<GuestController> _logger;
        private readonly IMediator _mediator;
        public List<ErrorCodes> errorCodesList = new List<ErrorCodes> { ErrorCodes.GUEST_NOT_FOUND,
                                                                      ErrorCodes.GUEST_COULDNOT_STORE_DATA,
                                                                      ErrorCodes.GUEST_INVALID_ID_PERSON,
                                                                      ErrorCodes.GUEST_MISSING_REQUERED_INFORMATION,
                                                                      ErrorCodes.GUEST_INVALID_EMAIL,
                                                                    };
        public GuestController(ILogger<GuestController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<GuestDTO>> Post(GuestDTO guest)
        {
            var command = new CreateGuestCommand
            {
                GuestDTO = guest
            };

            var res = await _mediator.Send(command);

            if (res.Success) return Created("", res.Data);

            if (errorCodesList.Contains(res.ErrorCode)) return BadRequest(res);

            _logger.LogError("Response with unknow ErrorCode Returned", res);
            return BadRequest(500);
        }

        [HttpGet]
        public async Task<ActionResult<GuestDTO>> Get(int id)
        {
            var query = new GetGuestQuery
            {
                Id = id
            };

            var res = await _mediator.Send(query);

            if (res.Success) { return Created("", res.Data); }

            return NotFound(res);
        }
    }
}
