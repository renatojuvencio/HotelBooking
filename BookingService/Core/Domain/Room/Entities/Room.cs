using Domain.Guest.ValueObjects;
using Domain.Room.Exceptions;
using Domain.Room.Ports;

namespace Domain.Room.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintenace { get; set; }
        public Price Price { get; set; }
        public bool IsAvailable
        {
            get
            {
                if (InMaintenace || HasGuest)
                {
                    return false;
                }
                return true;
            }
        }

        public bool HasGuest
        {
            get { return true; }
        }

        private void ValidState()
        {
            if (string.IsNullOrEmpty(Name) ||
               string.IsNullOrWhiteSpace(Name) ||
               Level == null ||
               Price == null)
            {
                throw new MissingRequeredInformationException();
            }
        }
        public async Task Save(IRoomRepository roomRepository)
        {
            if (Id == 0)
            {
                await roomRepository.Create(this);
            }
            else
            {
                await roomRepository.Create(this);
            }
        }
    }
}
