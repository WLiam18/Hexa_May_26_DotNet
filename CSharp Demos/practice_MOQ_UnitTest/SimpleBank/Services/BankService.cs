using SimpleBank.Interfaces;

namespace SimpleBank.Services
{
    public class BankService
    {
        private readonly IAccountRepository _accountRepo;
        private readonly INotificationService _notification;

        public BankService(IAccountRepository accountRepo, INotificationService notification)
        {
            _accountRepo = accountRepo;
            _notification = notification;
        }

        public string Withdraw(int accountId, decimal amount)
        {
            // Check if account exists
            if (!_accountRepo.AccountExists(accountId))
            {
                return "Account not found";
            }

            // Check if amount is valid
            if (amount <= 0)
            {
                return "Amount must be greater than zero";
            }

            // Check  balance
            decimal balance = _accountRepo.GetBalance(accountId);
            if (balance < amount)
            {
                return "Insufficient balance";
            }

            // Process withdrawal
            decimal newBalance = balance - amount;
            _accountRepo.UpdateBalance(accountId, newBalance);
            _notification.Send($"Withdrawn: ${amount}. New balance: ${newBalance}");

            return "Withdrawal successful";
        }
    }
}