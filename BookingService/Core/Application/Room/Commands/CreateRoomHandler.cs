using Application.Room.DTOs;
using Application.Room.Responses;
using Domain.Room.Exceptions;
using Domain.Room.Ports;
using MediatR;

namespace Application.Room.Commands
{
    public class CreateRoomHandler : IRequestHandler<CreateRoomCommand, RoomResponse>
    {
        private IRoomRepository _roomRepository;

        public CreateRoomHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public async Task<RoomResponse> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var room = RoomDTO.MapToEntity(request.RoomDTO);
                await room.Save(_roomRepository);
                request.RoomDTO.Id = room.Id;
                return new RoomResponse
                {
                    Data = request.RoomDTO,
                    Success = true,
                };
            }
            catch (InvalidRoomDataException e)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.ROOM_MISSING_REQUERED_INFORMATION,
                    Message = "Missing required information passed"
                };
            }
            catch (Exception)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.ROOM_COULDNOT_STORE_DATA,
                    Message = "There was an error when saving to DB"
                };

            }
        }
    }
}
