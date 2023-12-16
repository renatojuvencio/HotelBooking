using Application.Guest.DTOs;
using Application.Guest.Ports;
using Application.Guest.Requests;
using Application.Guest.Responses;
using Domain.Ports;

namespace Application
{
    public class GuestManager : IGuestManager
    {
        private IGuestRepository _guestRepository;
        public GuestManager(IGuestRepository guestRepository) 
        {
            _guestRepository = guestRepository;
        }
        public async Task<GuestResponse> CreateGuest(CreateGuestRequest request)
        {
            try
            {
                var guest = GuestDTO.MapToEntity(request.Data);
                await guest.Save(_guestRepository);
                request.Data.Id = guest.Id;
                return new GuestResponse
                {
                    Data = request.Data,
                    Success = true,
                };
            }
            catch (Exception)
            {

                return new GuestResponse
                {
                    Data = request.Data,
                    Success = false,
                    ErrorCode = ErrorCode.COULDNOT_STORE_DATA,
                    Message = "There was an error when saving to DB"
                };
            }
        }
    }
}
