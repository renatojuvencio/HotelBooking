using Domain.Guest.Enums;
using Action = Domain.Guest.Enums.Action;

namespace Domain.Booking.Entities
{
    public class Booking
    {
        public Booking()
        {
            Status = Status.Created;
        }
        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Room.Entities.Room Room { get; set; }
        public Guest.Entities.Guest Guest { get; set; }
        private Status Status { get; set; }
        public Status CurrentStatus
        {
            get { return Status; }
        }
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
    }
}
