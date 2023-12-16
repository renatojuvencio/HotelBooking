﻿using Application.Guest.DTOs;
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

        public GuestController(ILogger<GuestController> logger, IGuestManager guestManager)
        {
            _logger = logger;
            _guestManager = guestManager;
        }

        [HttpPost]
        public async Task<ActionResult<GuestDTO>> Post (GuestDTO guest)
        {
            var request = new CreateGuestRequest
            {
                Data = guest
            };
            var res = await _guestManager.CreateGuest(request);
            
            if (res.Success) return Created("", res.Data);

            if (res.ErrorCode == Application.ErrorCode.NOT_FOUND) return BadRequest(res);

            _logger.LogError("Response with unknow ErrorCode Returned", res);
            return BadRequest(500);
        }
    }
}
