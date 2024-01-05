using Application.Guest.DTOs;
using Application.Guest.Responses;
using Domain.Guest.Exceptions;
using Domain.Guest.Ports;
using MediatR;

namespace Application.Guest.Commands
{
    public class CreateGuestHandler : IRequestHandler<CreateGuestCommand, GuestResponse>
    {
        private IGuestRepository _guestRepository;

        public CreateGuestHandler(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task<GuestResponse> Handle(CreateGuestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var guest = GuestDTO.MapToEntity(request.GuestDTO);
                await guest.Save(_guestRepository);
                request.GuestDTO.Id = guest.Id;
                return new GuestResponse
                {
                    Data = request.GuestDTO,
                    Success = true,
                };
            }

            catch (InvalidEmailException e)
            {

                return new GuestResponse
                {
                    Data = request.GuestDTO,
                    Success = false,
                    ErrorCode = ErrorCodes.GUEST_INVALID_EMAIL,
                    Message = "The given email is not valid"
                };
            }

            catch (InvalidPersonDocumentIdException e)
            {

                return new GuestResponse
                {
                    Data = request.GuestDTO,
                    Success = false,
                    ErrorCode = ErrorCodes.GUEST_INVALID_ID_PERSON,
                    Message = "The ID passed is not valid"
                };
            }

            catch (MissingRequiredInformationException e)
            {

                return new GuestResponse
                {
                    Data = request.GuestDTO,
                    Success = false,
                    ErrorCode = ErrorCodes.GUEST_MISSING_REQUERED_INFORMATION,
                    Message = "There was an error when saving to DB"
                };
            }
            catch (Exception)
            {

                return new GuestResponse
                {
                    Data = request.GuestDTO,
                    Success = false,
                    ErrorCode = ErrorCodes.GUEST_COULDNOT_STORE_DATA,
                    Message = "There was an error when saving to DB"
                };
            }
        }
    }
}
