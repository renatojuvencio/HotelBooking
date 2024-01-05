using Domain.Room.Ports;
using Microsoft.EntityFrameworkCore;

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

        public Task<Domain.Room.Entities.Room> Get(int Id)
        {
            return _hotelDBContext.Rooms
                .Where(g => g.Id == Id).FirstOrDefaultAsync();
        }

        public Task<Domain.Room.Entities.Room> GetAggregate(int Id)
        {
            return _hotelDBContext.Rooms
                .Include(r => r.Bookings)
                .Where(g => g.Id == Id).FirstAsync();
        }
    }
}
