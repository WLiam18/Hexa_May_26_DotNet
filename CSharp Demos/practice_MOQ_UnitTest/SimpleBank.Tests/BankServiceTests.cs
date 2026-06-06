using Moq;
using NUnit.Framework;
using SimpleBank.Interfaces;
using SimpleBank.Services;

namespace SimpleBank.Tests
{
    [TestFixture]
    public class BankServiceTests
    {
        private Mock<IAccountRepository> _mockRepo;
        private Mock<INotificationService> _mockNotify;
        private BankService _bankService;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IAccountRepository>();
            _mockNotify = new Mock<INotificationService>();
            _bankService = new BankService(_mockRepo.Object, _mockNotify.Object);
        }

        [Test]
        public void Withdraw_ValidAccount_SufficientBalance_ReturnsSuccess()
        {
            // Arrange
            _mockRepo.Setup(x => x.AccountExists(101)).Returns(true);
            _mockRepo.Setup(x => x.GetBalance(101)).Returns(1000);

            // Act
            string result = _bankService.Withdraw(101, 200);

            // Assert
            Assert.That(result, Is.EqualTo("Withdrawal successful"));
            _mockRepo.Verify(x => x.UpdateBalance(101, 800), Times.Once);
            _mockNotify.Verify(x => x.Send("Withdrawn: $200. New balance: $800"), Times.Once);
        }

        [Test]
        public void Withdraw_AccountNotFound_ReturnsError()
        {
            // Arrange
            _mockRepo.Setup(x => x.AccountExists(999)).Returns(false);

            // Act
            string result = _bankService.Withdraw(999, 100);

            // Assert
            Assert.That(result, Is.EqualTo("Account not found"));
            _mockRepo.Verify(x => x.UpdateBalance(It.IsAny<int>(), It.IsAny<decimal>()), Times.Never);
            _mockNotify.Verify(x => x.Send(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void Withdraw_ZeroAmount_ReturnsError()
        {
            // Arrange
            _mockRepo.Setup(x => x.AccountExists(101)).Returns(true);
            _mockRepo.Setup(x => x.GetBalance(101)).Returns(1000);

            // Act
            string result = _bankService.Withdraw(101, 0);

            // Assert
            Assert.That(result, Is.EqualTo("Amount must be greater than zero"));
            _mockRepo.Verify(x => x.UpdateBalance(It.IsAny<int>(), It.IsAny<decimal>()), Times.Never);
            _mockNotify.Verify(x => x.Send(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void Withdraw_InsufficientBalance_ReturnsError()
        {
            // Arrange - Balance only 100
            _mockRepo.Setup(x => x.AccountExists(101)).Returns(true);
            _mockRepo.Setup(x => x.GetBalance(101)).Returns(100);

            // Act - Try to withdraw 500
            string result = _bankService.Withdraw(101, 500);

            // Assert
            Assert.That(result, Is.EqualTo("Insufficient balance"));
            _mockRepo.Verify(x => x.UpdateBalance(It.IsAny<int>(), It.IsAny<decimal>()), Times.Never);
            _mockNotify.Verify(x => x.Send(It.IsAny<string>()), Times.Never);
        }
    }
}