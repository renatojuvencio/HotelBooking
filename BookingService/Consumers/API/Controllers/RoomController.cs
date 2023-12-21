using Application.Guest.DTOs;
using Application.Guest.Ports;
using Application.Guest.Requests;
using Application;
using Microsoft.AspNetCore.Mvc;
using Application.Room.DTOs;
using Application.Room.Requests;
using Application.Room.Ports;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly ILogger<GuestController> _logger;
        private readonly IRoomManager _roomManager;
        public List<ErrorCode> errorCodesList = new List<ErrorCode> { ErrorCode.ROOM_NOT_FOUND,
                                                                      ErrorCode.ROOM_COULDNOT_STORE_DATA,
                                                                      ErrorCode.ROOM_INVALID_ID_PERSON,
                                                                      ErrorCode.ROOM_MISSING_REQUERED_INFORMATION,
                                                                      ErrorCode.ROOM_INVALID_EMAIL,
                                                                    };
        public RoomController(ILogger<GuestController> logger, IRoomManager roomManager)
        {
            _logger = logger;
            _roomManager = roomManager;
        }

        [HttpPost]
        public async Task<ActionResult<RoomDTO>> Post(RoomDTO room)
        {
            var request = new CreateRoomRequest
            {
                Data = room
            };
            var res = await _roomManager.CreateRoom(request);

            if (res.Success) return Created("", res.Data);

            if (errorCodesList.Contains(res.ErrorCode)) return BadRequest(res);

            _logger.LogError("Response with unknow ErrorCode Returned", res);
            return BadRequest(500);
        }

        [HttpGet]
        public async Task<ActionResult<RoomDTO>> Get(int id)
        {
            var res = await _roomManager.GetRoom(id);

            if (res.Success) { return Created("", res.Data); }

            return NotFound(res);
        }
    }
}
