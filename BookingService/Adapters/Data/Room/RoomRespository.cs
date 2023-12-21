using Domain.Room.Ports;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Room
{
    public class RoomRespository : IRoomRepository
    {
        private readonly HotelDBContext _hotelDBContext;

        public RoomRespository(HotelDBContext hotelDBContext)
        {
            _hotelDBContext = hotelDBContext;
        }

        public async Task<int> Create(Domain.Room.Entities.Room room)
        {
            _hotelDBContext.Rooms.Add(room);
            await _hotelDBContext.SaveChangesAsync();
            return room.Id;
        }

        public async Task<Domain.Room.Entities.Room> Get(int id)
        {
            return await _hotelDBContext.Rooms.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
