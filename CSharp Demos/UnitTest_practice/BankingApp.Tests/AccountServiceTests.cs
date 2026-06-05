using NUnit.Framework;
using BankingApp.Models;
using BankingApp.Services;

namespace BankingApp.Tests
{
    [TestFixture]
    public class AccountServiceTests
    {
        private AccountService _service;
        private Account _account;

        [SetUp]
        public void SetUp()
        {
            _service = new AccountService();
            _account = new Account(1, "John", 5000);
        }

        // ========== CREATE ACCOUNT TESTS ==========

        [Test]
        public void CreateAccount_ValidData_ReturnsAccount()
        {
            Account account = _service.CreateAccount(2, "Jane", 3000);

            Assert.That(account, Is.Not.Null);
            Assert.That(account.Id, Is.EqualTo(2));
            Assert.That(account.Name, Is.EqualTo("Jane"));
            Assert.That(account.Balance, Is.EqualTo(3000));
        }

        [Test]
        public void CreateAccount_InvalidId_ThrowsException()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                _service.CreateAccount(0, "John", 5000));

            Assert.That(ex.Message, Is.EqualTo("Id must be greater than 0"));
        }

        [Test]
        public void CreateAccount_EmptyName_ThrowsException()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                _service.CreateAccount(1, "", 5000));

            Assert.That(ex.Message, Is.EqualTo("Name cannot be empty"));
        }

        [Test]
        public void CreateAccount_NegativeBalance_ThrowsException()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                _service.CreateAccount(1, "John", -100));

            Assert.That(ex.Message, Is.EqualTo("Opening balance cannot be negative"));
        }

        // ========== DEPOSIT TESTS ==========

        [Test]
        public void Deposit_ValidAmount_IncreasesBalance()
        {
            _service.Deposit(_account, 1000);
            Assert.That(_account.Balance, Is.EqualTo(6000));
        }

        [TestCase(100, 5100)]
        [TestCase(500, 5500)]
        [TestCase(1000, 6000)]
        public void Deposit_MultipleAmounts_UpdatesBalanceCorrectly(decimal amount, decimal expected)
        {
            _service.Deposit(_account, amount);
            Assert.That(_account.Balance, Is.EqualTo(expected));
        }

        [Test]
        public void Deposit_ZeroAmount_ThrowsException()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                _service.Deposit(_account, 0));

            Assert.That(ex.Message, Is.EqualTo("Deposit amount must be greater than 0"));
        }

        [Test]
        public void Deposit_NegativeAmount_ThrowsException()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                _service.Deposit(_account, -500));

            Assert.That(ex.Message, Is.EqualTo("Deposit amount must be greater than 0"));
        }

        [Test]
        public void Deposit_NullAccount_ThrowsException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
                _service.Deposit(null, 1000));

            Assert.That(ex.ParamName, Is.EqualTo("account"));
        }

        // ========== WITHDRAW TESTS ==========

        [Test]
        public void Withdraw_ValidAmount_DecreasesBalance()
        {
            _service.Withdraw(_account, 1000);
            Assert.That(_account.Balance, Is.EqualTo(4000));
        }

        [Test]
        public void Withdraw_ZeroAmount_ThrowsException()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                _service.Withdraw(_account, 0));

            Assert.That(ex.Message, Is.EqualTo("Withdraw amount must be greater than 0"));
        }

        [Test]
        public void Withdraw_NegativeAmount_ThrowsException()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                _service.Withdraw(_account, -500));

            Assert.That(ex.Message, Is.EqualTo("Withdraw amount must be greater than 0"));
        }

        [Test]
        public void Withdraw_AmountGreaterThanBalance_ThrowsException()
        {
            var ex = Assert.Throws<InvalidOperationException>(() =>
                _service.Withdraw(_account, 10000));

            Assert.That(ex.Message, Is.EqualTo("Insufficient balance"));
        }

        [Test]
        public void Withdraw_NullAccount_ThrowsException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
                _service.Withdraw(null, 1000));

            Assert.That(ex.ParamName, Is.EqualTo("account"));
        }

        // ========== GET BALANCE TESTS ==========

        [Test]
        public void GetBalance_ValidAccount_ReturnsBalance()
        {
            decimal balance = _service.GetBalance(_account);
            Assert.That(balance, Is.EqualTo(5000));
        }

        [Test]
        public void GetBalance_NullAccount_ThrowsException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
                _service.GetBalance(null));

            Assert.That(ex.ParamName, Is.EqualTo("account"));
        }

        // ========== TRANSFER TESTS ==========

        [Test]
        public void Transfer_ValidAmount_MovesMoneyBetweenAccounts()
        {
            Account from = new Account(1, "John", 5000);
            Account to = new Account(2, "Jane", 3000);

            _service.Transfer(from, to, 2000);

            Assert.That(from.Balance, Is.EqualTo(3000));
            Assert.That(to.Balance, Is.EqualTo(5000));
        }

        [Test]
        public void Transfer_AmountGreaterThanBalance_ThrowsException()
        {
            Account from = new Account(1, "John", 5000);
            Account to = new Account(2, "Jane", 3000);

            var ex = Assert.Throws<InvalidOperationException>(() =>
                _service.Transfer(from, to, 10000));

            Assert.That(ex.Message, Is.EqualTo("Insufficient balance"));
        }
    }
}