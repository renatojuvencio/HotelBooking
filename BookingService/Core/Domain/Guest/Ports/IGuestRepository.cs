using Domain.Entities;

namespace Domain.Guest.Ports
{
    public interface IGuestRepository
    {
        Task<Guest> Get(int Id);
        Task<int> Create(Guest guest);
    }
}
