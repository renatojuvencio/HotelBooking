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
        public async Task<Domain.Booking.Entities.Booking> CreateAsync(Domain.Booking.Entities.Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<Domain.Booking.Entities.Booking> GetAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.Guest)
                .Include(b => b.Room)
                .Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
