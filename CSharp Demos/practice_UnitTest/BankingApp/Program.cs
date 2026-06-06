using BankingApp.Models;
using BankingApp.Services;

namespace BankingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AccountService service = new AccountService();

            Account acc1 = service.CreateAccount(1, "John", 5000);
            Account acc2 = service.CreateAccount(2, "Jane", 3000);

            Console.WriteLine($"Account 1: {acc1.Name}, Balance: {acc1.Balance}");
            Console.WriteLine($"Account 2: {acc2.Name}, Balance: {acc2.Balance}");

            service.Deposit(acc1, 1000);
            Console.WriteLine($"After deposit: {acc1.Balance}");

            service.Withdraw(acc2, 500);
            Console.WriteLine($"After withdraw: {acc2.Balance}");

            service.Transfer(acc1, acc2, 2000);
            Console.WriteLine($"After transfer - Account1: {acc1.Balance}, Account2: {acc2.Balance}");
        }
    }
}