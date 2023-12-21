using Domain.Guest.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Guest
{
    public class GuestRepository : IGuestRepository
    {
        private readonly HotelDBContext _hotelDBContext;
        public GuestRepository(HotelDBContext hotelDBContext)
        {
            _hotelDBContext = hotelDBContext;
        }
        public async Task<int> Create(Domain.Entities.Guest guest)
        {
            _hotelDBContext.Guests.Add(guest);
            await _hotelDBContext.SaveChangesAsync();
            return guest.Id;
        }

        public async Task<Domain.Entities.Guest> Get(int Id)
        {
            return await _hotelDBContext.Guests.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }
    }
}
