namespace SimpleBank.Interfaces
{
    public interface IAccountRepository
    {
        bool AccountExists(int accountId);
        decimal GetBalance(int accountId);
        void UpdateBalance(int accountId, decimal newBalance);
    }
}