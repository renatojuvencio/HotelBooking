using Application;
using Application.Guest.DTOs;
using Application.Guest.Ports;
using Application.Guest.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuestController : ControllerBase
    {
        private readonly ILogger<GuestController> _logger;
        private readonly IGuestManager _guestManager;
        public List<ErrorCode> errorCodesList = new List<ErrorCode> { ErrorCode.NOT_FOUND,
                                                                      ErrorCode.COULDNOT_STORE_DATA,
                                                                      ErrorCode.INVALID_ID_PERSON,
                                                                      ErrorCode.MISSING_REQUERED_INFORMATION,
                                                                      ErrorCode.INVALID_EMAIL,
                                                                    };
        public GuestController(ILogger<GuestController> logger, IGuestManager guestManager)
        {
            _logger = logger;
            _guestManager = guestManager;
        }

        [HttpPost]
        public async Task<ActionResult<GuestDTO>> Post(GuestDTO guest)
        {
            var request = new CreateGuestRequest
            {
                Data = guest
            };
            var res = await _guestManager.CreateGuest(request);

            if (res.Success) return Created("", res.Data);

            if (errorCodesList.Contains(res.ErrorCode)) return BadRequest(res);

            _logger.LogError("Response with unknow ErrorCode Returned", res);
            return BadRequest(500);
        }

        [HttpGet]
        public async Task<ActionResult<GuestDTO>> Get(int id)
        {
            var res = await _guestManager.GetGuest(id);

            if (res.Success) { return Created("", res.Data); }

            return NotFound(res);
        }
    }
}
