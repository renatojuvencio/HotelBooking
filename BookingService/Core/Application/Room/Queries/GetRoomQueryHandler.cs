using Application.Room.DTOs;
using Application.Room.Responses;
using Domain.Room.Ports;
using MediatR;

namespace Application.Room.Queries
{
    public class GetRoomQueryHandler : IRequestHandler<GetRoomQuery, RoomResponse>
    {
        private IRoomRepository _roomRepository;

        public GetRoomQueryHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public async Task<RoomResponse> Handle(GetRoomQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var room = await _roomRepository.Get(request.Id);
                if (room == null)
                {
                    return new RoomResponse
                    {
                        Success = false,
                        Message = "No record was found with the given id",
                        ErrorCode = ErrorCodes.ROOM_NOT_FOUND
                    };
                }
                return new RoomResponse
                {
                    Data = RoomDTO.MapToDto(room),
                    Success = true
                };
            }
            catch (Exception)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.ROOM_NOT_FOUND,
                    Message = $"Could not recovery room id {request.Id}"
                };
            }
        }
    }
}
