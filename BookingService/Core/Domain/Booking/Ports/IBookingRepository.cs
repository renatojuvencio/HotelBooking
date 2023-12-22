namespace Domain.Booking.Ports
{
    public interface IBookingRepository
    {
        Task<int> CreateAsync(Entities.Booking booking);
        Task<Entities.Booking> GetAsync(int id);
    }
}
