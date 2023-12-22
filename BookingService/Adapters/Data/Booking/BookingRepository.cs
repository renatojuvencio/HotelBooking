using Domain.Booking.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Booking
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelDBContext _context;
        public BookingRepository(HotelDBContext hotelDBContext) 
        {
            _context = hotelDBContext;
        }
        public async Task<int> CreateAsync(Domain.Booking.Entities.Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking.Id;
        }

        public async Task<Domain.Booking.Entities.Booking> GetAsync(int id)
        {
            return await _context.Bookings.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
