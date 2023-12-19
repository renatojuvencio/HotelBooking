using Application;
using Application.Guest.DTOs;
using Application.Guest.Requests;
using Domain.Entities;
using Domain.Ports;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using System.Security.Cryptography.X509Certificates;

namespace ApplicationTests 
{ 

    public class Tests
    {
        GuestManager guestManager;
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task CreateGuestSuccess()
        {
            var guestDTO = new GuestDTO
            {
                Name = "Fulano",
                Surname = "Silva",
                Email = "fulano@silva.com",
                IdNumber = "123456",
                IdTypeCode = 1
            };

            var expectedId = 333;

            var request = new CreateGuestRequest
            {
                Data = guestDTO,
            };

            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Create(
                                        It.IsAny<Guest>()))
                                        .Returns(Task.FromResult(expectedId));

            guestManager = new GuestManager(fakeRepo.Object);

            var res = await guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Success);
            Assert.AreEqual(res.Data.Id, expectedId);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("a")]
        [TestCase("ab")]
        [TestCase("abc")]
        public async Task Should_Return_InvalidIdPersonDocIDException_WhenDocAreInvalid(string docNumber)
        {
            var expectedId = 333;
            var fakeRepo = new Mock<IGuestRepository>();
            fakeRepo.Setup(x => x.Create(
                                        It.IsAny<Guest>()))
                                        .Returns(Task.FromResult(expectedId));
            guestManager = new GuestManager(fakeRepo.Object);
            var guestDTO = new GuestDTO
            {
                Name = "Fulano",
                Surname = "Silva",
                Email = "fulano@silva.com",
                IdNumber = docNumber,
                IdTypeCode = 1
            };
            var request = new CreateGuestRequest
            {
                Data = guestDTO,
            };
            var res = await guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.IsFalse(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCode.INVALID_ID_PERSON);
        }

        [TestCase("", "Juvencio", "renato@teste.com")]
        [TestCase(null, "Juvencio", "renato@teste.com")]

        [TestCase("", "Juvencio", "renato@teste.com")]
        [TestCase("", "Maria", "renato@teste.com")]
        [TestCase("", null, "renato@teste.com")]

        [TestCase("", "Juvencio", "maria@teste.com")]
        [TestCase("", "Juvencio", null)]

        [TestCase("   ", "Juvencio", "renato@teste.com")]
        [TestCase("   ", null, "renato@teste.com")]
        [TestCase("   ", "Juvencio", null)]
        public async Task Should_Return_MissingRequiredInformationException_WhenDocAreInvalid(string name, string surname, string email)
        {
            var expectedId = 333;
            var fakeRepo = new Mock<IGuestRepository>();
            fakeRepo.Setup(x => x.Create(
                                        It.IsAny<Guest>()))
                                        .Returns(Task.FromResult(expectedId));
            guestManager = new GuestManager(fakeRepo.Object);
            var guestDTO = new GuestDTO
            {
                Name = name,
                Surname = surname,
                Email = email,
                IdNumber = "123456",
                IdTypeCode = 1
            };
            var request = new CreateGuestRequest
            {
                Data = guestDTO,
            };
            var res = await guestManager.CreateGuest(request);
            Assert.IsNotNull(res);
            Assert.IsFalse(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCode.MISSING_REQUERED_INFORMATION);
        }
    }
}