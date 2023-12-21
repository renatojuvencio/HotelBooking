using Application.Room.DTOs;

namespace Application.Room.Responses
{
    public class RoomResponse : Response
    {
        public RoomDTO Data { get; set; }
    }
}
