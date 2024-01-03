using Application;
using Application.Booking.Dtos;
using Application.Booking.Ports;
using Application.Booking.Requests;
using Application.Payment.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingManager _bookingManager;
        public List<ErrorCodes> errorCodesList = new List<ErrorCodes> { ErrorCodes.BOOKING_COULDNOT_STORE_DATA,
                                                                      ErrorCodes.BOOKING_NOT_FOUND,
                                                                      ErrorCodes.BOOKING_MISSING_REQUERED_INFORMATION,
                                                                    };

        public BookingController(ILogger<BookingController> logger, IBookingManager bookingManager)
        {
            _bookingManager = bookingManager;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> Post(BookingDto booking)
        {
            var request = new CreateBookingRequest
            {
                Data = booking
            };
            var res = await _bookingManager.CreateBookingAsync(request);

            if (res.Success) return Created("", res.Data);

            if (errorCodesList.Contains(res.ErrorCode)) return BadRequest(res);

            _logger.LogError("Response with unknow ErrorCode Returned", res);
            return BadRequest(500);
        }

        [HttpGet]
        public async Task<ActionResult<BookingDto>> Get(int id)
        {
            var res = await _bookingManager.GetBokingAsync(id);

            if (res.Success) { return Created("", res.Data); }

            return NotFound(res);
        }

        [HttpPost]
        [Route("{bookingId}/Pay")]
        public async Task<ActionResult<PaymentResponse>> Pay(
            PaymentRequestDto paymentRequestDto, int bookingId)
        {
            paymentRequestDto.BookingId = bookingId;
            var res = await _bookingManager.PayForABooking(paymentRequestDto);
            if (res.Success) return Ok(res.Data);
            return BadRequest(res);
        }
    }
}
