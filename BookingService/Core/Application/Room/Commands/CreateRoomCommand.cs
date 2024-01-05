using Application.Room.DTOs;
using Application.Room.Responses;
using MediatR;

namespace Application.Room.Commands
{
    public class CreateRoomCommand : IRequest<RoomResponse>
    {
        public RoomDTO RoomDTO { get; set; }
    }
}
