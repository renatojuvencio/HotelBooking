namespace Domain.Room.Ports
{
    public interface IRoomRepository
    {
        Task<int> Create(Entities.Room room);
        Task<Entities.Room> Get(int id);
        Task<Entities.Room> GetAggregate(int Id);
    }
}
