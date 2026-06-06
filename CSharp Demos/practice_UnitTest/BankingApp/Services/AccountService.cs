using BankingApp.Models;

namespace BankingApp.Services
{
    public class AccountService
    {
        // Create account with validation
        public Account CreateAccount(int id, string name, decimal balance)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than 0");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty");

            if (balance < 0)
                throw new ArgumentException("Opening balance cannot be negative");

            return new Account(id, name, balance);
        }

        // Add money
        public void Deposit(Account account, decimal amount)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account), "Account cannot be null");

            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be greater than 0");

            account.Balance += amount;
        }

        // Withdraw money
        public void Withdraw(Account account, decimal amount)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account), "Account cannot be null");

            if (amount <= 0)
                throw new ArgumentException("Withdraw amount must be greater than 0");

            if (amount > account.Balance)
                throw new InvalidOperationException("Insufficient balance");

            account.Balance -= amount;
        }

        // Get balance
        public decimal GetBalance(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account), "Account cannot be null");

            return account.Balance;
        }

        // Transfer between accounts
        public void Transfer(Account from, Account to, decimal amount)
        {
            Withdraw(from, amount);
            Deposit(to, amount);
        }
    }
}