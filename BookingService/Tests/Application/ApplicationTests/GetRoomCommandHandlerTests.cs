using Application;
using Application.Room.Queries;
using Domain.Room.Entities;
using Domain.Room.Ports;
using Moq;

namespace ApplicationTests
{
    [TestFixture]
    internal class GetRoomCommandHandlerTests
    {
        [Test]
        public async Task Should_Return_Room()
        {
            var query = new GetRoomQuery
            {
                Id = 1,
            };

            var repoMock = new Mock<IRoomRepository>();
            var fakeRoom = new Room { Id = 1, };
            repoMock.Setup(x => x.Get(query.Id))
                .Returns(Task.FromResult(fakeRoom));

            var hanlder = new GetRoomQueryHandler(repoMock.Object);
            var res = await hanlder.Handle(query, CancellationToken.None);

            Assert.True(res.Success);
            Assert.AreEqual(1, res.Data.Id);
        }

        [Test]
        public async Task Should_Not_Return_Room()
        {
            var query = new GetRoomQuery
            {
                Id = 1,
            };

            var repoMock = new Mock<IRoomRepository>();

            var hanlder = new GetRoomQueryHandler(repoMock.Object);
            var res = await hanlder.Handle(query, CancellationToken.None);

            Assert.False(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCodes.ROOM_NOT_FOUND);
        }
    }
}
