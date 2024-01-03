using Application.Guest.DTOs;
using Application.Guest.Ports;
using Application.Guest.Requests;
using Application.Guest.Responses;
using Domain.Guest.Exceptions;
using Domain.Guest.Ports;

namespace Application.Guest
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

            catch (InvalidEmailException e)
            {

                return new GuestResponse
                {
                    Data = request.Data,
                    Success = false,
                    ErrorCode = ErrorCodes.GUEST_INVALID_EMAIL,
                    Message = "The given email is not valid"
                };
            }

            catch (InvalidPersonDocumentIdException e)
            {

                return new GuestResponse
                {
                    Data = request.Data,
                    Success = false,
                    ErrorCode = ErrorCodes.GUEST_INVALID_ID_PERSON,
                    Message = "The ID passed is not valid"
                };
            }

            catch (MissingRequiredInformationException e)
            {

                return new GuestResponse
                {
                    Data = request.Data,
                    Success = false,
                    ErrorCode = ErrorCodes.GUEST_MISSING_REQUERED_INFORMATION,
                    Message = "There was an error when saving to DB"
                };
            }
            catch (Exception)
            {

                return new GuestResponse
                {
                    Data = request.Data,
                    Success = false,
                    ErrorCode = ErrorCodes.GUEST_COULDNOT_STORE_DATA,
                    Message = "There was an error when saving to DB"
                };
            }
        }

        public async Task<GuestResponse> GetGuest(int guestId)
        {
            var guest = await _guestRepository.Get(guestId);
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
    }
}
