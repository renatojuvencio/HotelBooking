using Domain.Entities;
using Domain.Enums;
using NUnit.Framework;

namespace DomainTests.Bookings
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldAlwaysStartWithCreatedStatus()
        {
            var booking = new Booking();
            Assert.AreEqual(booking.CurrentStatus, Status.Created);
        }

        [Test]
        public void ShouldSetStatusToPaidWhenPayingForABookingCreatedStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Domain.Enums.Action.Pay);
            Assert.AreEqual(booking.CurrentStatus, Status.Paied);
        }

        [Test]
        public void ShouldSetStatusToCanceledWhenCancelingABookingWithCreatedStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Domain.Enums.Action.Cancel);
            Assert.AreEqual(booking.CurrentStatus, Status.Canceled);
        }

        [Test]
        public void ShouldSetStatusToFinishedWhenFinishingPaiedBooking()
        {
            var booking = new Booking();
            booking.ChangeState(Domain.Enums.Action.Pay);
            booking.ChangeState(Domain.Enums.Action.Finish);
            Assert.AreEqual(booking.CurrentStatus, Status.Finished);
        }

        [Test]
        public void ShouldSetStatusToRefoundedWhenRefoundedPaiedBooking()
        {
            var booking = new Booking();
            booking.ChangeState(Domain.Enums.Action.Pay);
            booking.ChangeState(Domain.Enums.Action.Refound);
            Assert.AreEqual(booking.CurrentStatus, Status.Refounded);
        }

        [Test]
        public void ShouldSetStatusToCreatedWhenReopendCanceledBooking()
        {
            var booking = new Booking();
            booking.ChangeState(Domain.Enums.Action.Cancel);
            booking.ChangeState(Domain.Enums.Action.Reopen);
            Assert.AreEqual(booking.CurrentStatus, Status.Created);
        }

        [Test]
        public void ShouldNotChangeStatusToRefoundedWhenABookingCreatedStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Domain.Enums.Action.Refound);
            Assert.AreEqual(booking.CurrentStatus, Status.Created);
        }

        [Test]
        public void ShouldNotChangeStatusToRefoundedWhenABookingFinishStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Domain.Enums.Action.Pay);
            booking.ChangeState(Domain.Enums.Action.Finish);
            booking.ChangeState(Domain.Enums.Action.Refound);
            Assert.AreEqual(booking.CurrentStatus, Status.Finished);
        }

        [Test]
        public void ShouldNotChangeStatusToCanceledWhenABookingFinishStatus()
        {
            var booking = new Booking();
            booking.ChangeState(Domain.Enums.Action.Pay);
            booking.ChangeState(Domain.Enums.Action.Finish);
            booking.ChangeState(Domain.Enums.Action.Cancel);
            Assert.AreEqual(booking.CurrentStatus, Status.Finished);
        }
    }
}