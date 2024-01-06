using Application;
using Application.Booking.Commands;
using Application.Booking.Dtos;
using Application.Booking.Queries;
using Application.Payment.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        public List<ErrorCodes> errorCodesList = new List<ErrorCodes> { ErrorCodes.BOOKING_COULDNOT_STORE_DATA,
                                                                      ErrorCodes.BOOKING_NOT_FOUND,
                                                                      ErrorCodes.BOOKING_MISSING_REQUERED_INFORMATION,
                                                                    };
        private readonly IMediator _mediator;
        public BookingController(ILogger<BookingController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> Post(BookingDto booking)
        {
            var command = new CreateBookingCommand
            {
                BookingDto = booking
            };
            var res = await _mediator.Send(command);

            if (res.Success) return Created("", res.Data);

            if (errorCodesList.Contains(res.ErrorCode)) return BadRequest(res);

            _logger.LogError("Response with unknow ErrorCode Returned", res);
            return BadRequest(500);
        }

        [HttpGet]
        public async Task<ActionResult<BookingDto>> Get(int id)
        {
            var query = new GetBookingQuery
            {
                id = id,
            };
            var res = await _mediator.Send(query);

            if (res.Success) { return Created("", res.Data); }
            _logger.LogError("Could not proccess the request", res);
            return NotFound(res);
        }

        [HttpPost]
        [Route("{bookingId}/Pay")]
        public async Task<ActionResult<PaymentResponse>> Pay(PaymentRequestDto paymentRequestDto, int bookingId)
        {
            paymentRequestDto.BookingId = bookingId;
            var command = new PayForABookingCommand
            {
                PaymentRequestDto = paymentRequestDto
            };
            var res = await _mediator.Send(command);
            if (res.Success) return Ok(res.Data);
            return BadRequest(res);
        }
    }
}
