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
        public ICollection<Booking.Entities.Booking> Bookings { get; set; }
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
            get
            {
                var notavaiableStatus = new List<Guest.Enums.Status>()
                { Guest.Enums.Status.Created,
                    Guest.Enums.Status.Paied
                };

                return this.Bookings?.Where(
                    x => x.Room.Id == this.Id &&
                    notavaiableStatus.Contains(x.Status)).Count() > 0;
            }
        }

        private void ValidState()
        {
            if (string.IsNullOrEmpty(this.Name) ||
               string.IsNullOrWhiteSpace(this.Name) ||
               this.Level == null ||
               this.Price == null)
            {
                throw new InvalidRoomDataException();
            }
        }

        public bool ValidRoom()
        {
            ValidState();

            if (!this.IsAvailable)
                return false;

            return true;
        }
        public async Task Save(IRoomRepository roomRepository)
        {
            ValidState();
            if (Id == 0)
            {
                this.Id = await roomRepository.Create(this);
            }
            else
            {
            }
        }
    }
}
