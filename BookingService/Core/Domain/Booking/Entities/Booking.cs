using Domain.Booking.Exceptions;
using Domain.Booking.Ports;
using Domain.Guest.Enums;
using System;
using Action = Domain.Guest.Enums.Action;

namespace Domain.Booking.Entities
{
    public class Booking
    {
        public Booking()
        {
            Status = Status.Created;
            PlacedAt = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Room.Entities.Room Room { get; set; }
        public Guest.Entities.Guest Guest { get; set; }
        public Status Status { get; set; }
        public void ChangeState(Action action)
        {
            Status = (Status, action) switch
            {
                (Status.Created, Action.Pay) => Status.Paied,
                (Status.Created, Action.Cancel) => Status.Canceled,
                (Status.Paied, Action.Finish) => Status.Finished,
                (Status.Paied, Action.Refound) => Status.Refounded,
                (Status.Canceled, Action.Reopen) => Status.Created,
                _ => Status
            };
        }

        private void ValidState()
        {
            if (this.PlacedAt == default(DateTime))
                throw new PlacedAtIsARequiredInformationException();

            if (this.Start == default(DateTime))
                throw new StartIsARequiredInformationException();

            if (this.End == default(DateTime))
                throw new EndIsARequiredInformationException();

            if (this.Guest == null)
                throw new GuestIsARequiredInformationException();

            if(this.Room == null)
                throw new RoomIsARequiredInformationException();

        }

        public async Task SaveAsync(IBookingRepository bookingRepository)
        {
            ValidState();
            this.Guest.ValidGuest();
            this.Room.ValidRoom();

            if(this.Id == 0)
            {
                var newBookingId = await bookingRepository.CreateAsync(this);
                this.Id = newBookingId;
            }
            else
            {

            }
        }
    }
}
