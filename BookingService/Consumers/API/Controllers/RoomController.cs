using Application;
using Application.Room.Commands;
using Application.Room.DTOs;
using Application.Room.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IMediator _mediator;
        public List<ErrorCodes> errorCodesList = new List<ErrorCodes> { ErrorCodes.ROOM_NOT_FOUND,
                                                                      ErrorCodes.ROOM_COULDNOT_STORE_DATA,
                                                                      ErrorCodes.ROOM_MISSING_REQUERED_INFORMATION,
                                                                    };
        public RoomController(ILogger<RoomController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<RoomDTO>> Post(RoomDTO room)
        {
            var command = new CreateRoomCommand
            {
                RoomDTO = room
            };

            var res = await _mediator.Send(command);

            if (res.Success) return Created("", res.Data);

            if (errorCodesList.Contains(res.ErrorCode)) return BadRequest(res);

            _logger.LogError("Response with unknow ErrorCode Returned", res);
            return BadRequest(500);
        }

        [HttpGet]
        public async Task<ActionResult<RoomDTO>> Get(int id)
        {
            var command = new GetRoomQuery
            {
                Id = id,
            };
            var res = await _mediator.Send(command);

            if (res.Success) { return Created("", res.Data); }

            return NotFound(res);
        }
    }
}
