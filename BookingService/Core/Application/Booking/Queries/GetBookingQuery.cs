﻿using Application.Booking.Dtos;
using Application.Booking.Response;
using MediatR;

namespace Application.Booking.Queries
{
    public class GetBookingQuery : IRequest<BookingResponse>
    {
        public int id { get; set; }
    }
}