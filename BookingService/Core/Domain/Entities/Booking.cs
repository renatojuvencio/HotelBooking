using Domain.Guest.Enums;
using Action = Domain.Guest.Enums.Action;

namespace Domain.Entities
{
    public class Booking
    {
        public Booking() 
        {
            this.Status = Status.Created;
        }
        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start {  get; set; }
        public DateTime End { get; set; }
        public Room Room { get; set; }
        public Guest Guest { get; set; }
        private Status Status { get; set; }
        public Status CurrentStatus
        {
            get { return this.Status; }
        }
        public void ChangeState(Action action)
        {
            this.Status = (this.Status, action) switch
            {
                (Status.Created,    Action.Pay)     => Status.Paied,
                (Status.Created,    Action.Cancel)  => Status.Canceled,
                (Status.Paied,      Action.Finish)  => Status.Finished,
                (Status.Paied,      Action.Refound) => Status.Refounded,
                (Status.Canceled,   Action.Reopen)  => Status.Created,
                _ => this.Status
            };
        }
    }
}
