using System;

namespace OpenClosePrinciple
{
    /*
    open close definition
    
    "Software entities (classes, modules, functions, etc.) 
    should be open for extension but closed for modification."
    
    OPEN FOR EXTENSION  - You can add new functionality
    CLOSED FOR MODIFICATION - You don't change existing code
    
    */
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("OPEN/CLOSE PRINCIPLE DEMO\n");
            
            
            Console.WriteLine("--- GOOD CODE (Follows OCP) ---");
            Console.WriteLine("Each customer type is a separate class.");
            Console.WriteLine("To add new type, just add new class.\n");
            
            Customer regular = new RegularCustomer();
            Customer premium = new PremiumCustomer();
            Customer vip = new VIPCustomer();
            
            double amount = 1000;
            
            Console.WriteLine($"Original Amount: {amount}");
            Console.WriteLine($"Regular Customer Discounted Price: {regular.GetDiscountedPrice(amount)}");
            Console.WriteLine($"Premium Customer Discounted Price: {premium.GetDiscountedPrice(amount)}");
            Console.WriteLine($"VIP Customer Discounted Price: {vip.GetDiscountedPrice(amount)}");
            
            // NEW TYPE - Just add class! No existing code changed
            Customer gold = new GoldCustomer();
            Console.WriteLine($"Gold Customer Discounted Price: {gold.GetDiscountedPrice(amount)}");
            
            Console.WriteLine("\n");
            
          
            /*
            //  BAD CODE - DO NOT USE THIS PATTERN
            
            Console.WriteLine("--- BAD CODE OUTPUT (if uncommented) ---");
            BadDiscountCalculator badCalc = new BadDiscountCalculator();
            
            Console.WriteLine($"Regular: {badCalc.Calculate("Regular", 1000)}");
            Console.WriteLine($"Premium: {badCalc.Calculate("Premium", 1000)}");
            Console.WriteLine($"VIP: {badCalc.Calculate("VIP", 1000)}");
            
           
            
            public class BadDiscountCalculator
            {
                // This method changes every time we add a new customer type
                public double Calculate(string customerType, double amount)
                {
                    if (customerType == "Regular")
                    {
                        return amount * 0.95;  // 5% discount
                    }
                    else if (customerType == "Premium")
                    {
                        return amount * 0.85;  // 15% discount
                    }
                    else if (customerType == "VIP")
                    {
                        return amount * 0.75;  // 25% discount
                    }
                    // To add Gold: Add another else-if here! (BAD)
                    // else if (customerType == "Gold")
                    // {
                    //     return amount * 0.80;
                    // }
                    else
                    {
                        return amount;
                    }
                }
            }
            */
            
            Console.ReadLine();
        }
    }
    
    // Following Open/Close Principle)
    
    // Base class - OPEN for extension
    public class Customer
    {
        // Virtual method - can be overridden by child classes
        public virtual double GetDiscountedPrice(double amount)
        {
            return amount;  // No discount by default
        }
    }
    
    // EXTENSION 1 - No existing code modified
    public class RegularCustomer : Customer
    {
        public override double GetDiscountedPrice(double amount)
        {
            return amount * 0.95;  // 5% discount
        }
    }
    
    // EXTENSION 2 - No existing code modified
    public class PremiumCustomer : Customer
    {
        public override double GetDiscountedPrice(double amount)
        {
            return amount * 0.85;  // 15% discount
        }
    }
    
    // EXTENSION 3 - No existing code modified
    public class VIPCustomer : Customer
    {
        public override double GetDiscountedPrice(double amount)
        {
            return amount * 0.75;  // 25% discount
        }
    }
    
    // NEW EXTENSION - Just ADD this class! No existing code changed! ✅
    public class GoldCustomer : Customer
    {
        public override double GetDiscountedPrice(double amount)
        {
            return amount * 0.80;  // 20% discount
        }
    }
}