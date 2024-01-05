using Application.Guest.DTOs;
using Application.Guest.Responses;
using MediatR;

namespace Application.Guest.Commands
{
    public class CreateGuestCommand : IRequest<GuestResponse>
    {
        public GuestDTO GuestDTO { get; set; }
    }
}
