using Application.Guest.DTOs;
using Application.Guest.Responses;
using Data.Guest;
using MediatR;

namespace Application.Guest.Queries
{
    public class GetGuestQueryHandler : IRequestHandler<GetGuestQuery, GuestResponse>
    {
        private GuestRepository _guestRepository;

        public GetGuestQueryHandler(GuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task<GuestResponse> Handle(GetGuestQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var guest = await _guestRepository.Get(request.Id);
                if (guest == null)
                {
                    return new GuestResponse
                    {
                        Success = false,
                        ErrorCode = ErrorCodes.GUEST_NOT_FOUND,
                        Message = "No record was found with the given id"
                    };
                }
                return new GuestResponse
                {
                    Data = GuestDTO.MapToDto(guest),
                    Success = true
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
