namespace Domain.Booking.Ports
{
    public interface IBookingRepository
    {
        Task<Entities.Booking> CreateAsync(Entities.Booking booking);
        Task<Entities.Booking> GetAsync(int id);
    }
}
