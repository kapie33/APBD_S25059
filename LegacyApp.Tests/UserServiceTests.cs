using Moq;
using NUnit.Framework;
using System;
using LegacyApp;

namespace LegacyApp.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserService _userService;
        private Mock<IClientRepository> _mockClientRepository;
        private Mock<IUserCreditService> _mockUserCreditService;

        [SetUp]
        public void Setup()
        {
            _mockClientRepository = new Mock<IClientRepository>();
            _mockUserCreditService = new Mock<IUserCreditService>();
            _userService = new UserService(_mockClientRepository.Object, _mockUserCreditService.Object);
        }

        [Test]
        public void AddUser_ShouldReturnFalse_WhenUserIsUnderAge()
        {
            var dateOfBirth = DateTime.Now.AddYears(-20);
            _mockClientRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Client { Type = "NormalClient" });
            var result = _userService.AddUser("John", "Doe", "john.doe@example.com", dateOfBirth, 1);
            Assert.That(result, Is.False);
        }

        [Test]
        public void AddUser_ShouldReturnTrue_WhenAllConditionsMet()
        {
            var dateOfBirth = DateTime.Now.AddYears(-30);
            _mockClientRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Client { Type = "ImportantClient" });
            _mockUserCreditService.Setup(x => x.GetCreditLimit(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1000);
            var result = _userService.AddUser("John", "Doe", "john.doe@example.com", dateOfBirth, 1);
            Assert.That(result, Is.True);
        }

        [Test]
        public void AddUser_ShouldReturnFalse_WhenEmailIsInvalid()
        {

            var dateOfBirth = DateTime.Now.AddYears(-30);
            _mockClientRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Client { Type = "ImportantClient" });

            var result = _userService.AddUser("John", "Doe", "johndoe", dateOfBirth, 1);

            Assert.That(result, Is.False);
        }
    }
}
