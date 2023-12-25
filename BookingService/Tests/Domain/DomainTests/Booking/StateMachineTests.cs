using Domain.Guest.Enums;
using NUnit.Framework;

namespace DomainTests.Booking
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
            var booking = new Domain.Booking.Entities.Booking();
            Assert.AreEqual(booking.Status, Status.Created);
        }

        [Test]
        public void ShouldSetStatusToPaidWhenPayingForABookingCreatedStatus()
        {
            var booking = new Domain.Booking.Entities.Booking();
            booking.ChangeState(Domain.Guest.Enums.Action.Pay);
            Assert.AreEqual(booking.Status, Status.Paied);
        }

        [Test]
        public void ShouldSetStatusToCanceledWhenCancelingABookingWithCreatedStatus()
        {
            var booking = new Domain.Booking.Entities.Booking();
            booking.ChangeState(Domain.Guest.Enums.Action.Cancel);
            Assert.AreEqual(booking.Status, Status.Canceled);
        }

        [Test]
        public void ShouldSetStatusToFinishedWhenFinishingPaiedBooking()
        {
            var booking = new Domain.Booking.Entities.Booking();
            booking.ChangeState(Domain.Guest.Enums.Action.Pay);
            booking.ChangeState(Domain.Guest.Enums.Action.Finish);
            Assert.AreEqual(booking.Status, Status.Finished);
        }

        [Test]
        public void ShouldSetStatusToRefoundedWhenRefoundedPaiedBooking()
        {
            var booking = new Domain.Booking.Entities.Booking();
            booking.ChangeState(Domain.Guest.Enums.Action.Pay);
            booking.ChangeState(Domain.Guest.Enums.Action.Refound);
            Assert.AreEqual(booking.Status, Status.Refounded);
        }

        [Test]
        public void ShouldSetStatusToCreatedWhenReopendCanceledBooking()
        {
            var booking = new Domain.Booking.Entities.Booking();
            booking.ChangeState(Domain.Guest.Enums.Action.Cancel);
            booking.ChangeState(Domain.Guest.Enums.Action.Reopen);
            Assert.AreEqual(booking.Status, Status.Created);
        }

        [Test]
        public void ShouldNotChangeStatusToRefoundedWhenABookingCreatedStatus()
        {
            var booking = new Domain.Booking.Entities.Booking();
            booking.ChangeState(Domain.Guest.Enums.Action.Refound);
            Assert.AreEqual(booking.Status, Status.Created);
        }

        [Test]
        public void ShouldNotChangeStatusToRefoundedWhenABookingFinishStatus()
        {
            var booking = new Domain.Booking.Entities.Booking();
            booking.ChangeState(Domain.Guest.Enums.Action.Pay);
            booking.ChangeState(Domain.Guest.Enums.Action.Finish);
            booking.ChangeState(Domain.Guest.Enums.Action.Refound);
            Assert.AreEqual(booking.Status, Status.Finished);
        }

        [Test]
        public void ShouldNotChangeStatusToCanceledWhenABookingFinishStatus()
        {
            var booking = new Domain.Booking.Entities.Booking();
            booking.ChangeState(Domain.Guest.Enums.Action.Pay);
            booking.ChangeState(Domain.Guest.Enums.Action.Finish);
            booking.ChangeState(Domain.Guest.Enums.Action.Cancel);
            Assert.AreEqual(booking.Status, Status.Finished);
        }
    }
}