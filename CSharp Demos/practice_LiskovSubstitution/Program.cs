using System;

namespace LiskovPractice
{
    // Liskov Substitution Principle: Child classes must be replaceable for their parent classes without breaking the program.

    class Program
    {
        static void Main(string[] args)
        {
            IPayment payment = new CreditCard();
            Console.WriteLine($"Credit Card: {payment.Pay(100)}");

            payment = new DebitCard();
            Console.WriteLine($"Debit Card: {payment.Pay(100)}");

            payment = new Upi();
            Console.WriteLine($"UPI: {payment.Pay(100)}");
        }
    }

    public interface IPayment
    {
        double Pay(double amount);
    }

    public class CreditCard : IPayment
    {
        public double Pay(double amount)
        {
            return amount + (amount * 0.02);
        }
    }

    public class DebitCard : IPayment
    {
        public double Pay(double amount)
        {
            return amount;
        }
    }

    public class Upi : IPayment
    {
        public double Pay(double amount)
        {
            return amount + (amount * 0.03);
        }
    }
}