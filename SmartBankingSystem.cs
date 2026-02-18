// using System;

// public class BankException : Exception
// {
//     public BankException(string message) : base (message){ }
// }

// public class InsufficientBalanceException : BankException
// {
//     public InsufficientBalanceException(string message) : base(message){ }
// }

// public class MinimumBalanceException : BankException
// {
//     public MinimumBalanceException(string message) : base(message){ }
// }

// public class InvalidTransactionException : BankException
// {
//     public InvalidTransactionException(string message) : base(message){ }
// }

// public abstract class BankAccount
// {
//     public int AccountNumber { get; set; }
//     public string CustomerName { get; set; }
//     public double Balance { get; set; }

//     public BankAccount(int accountNumber, string customerName, double balance)
//     {
//         this.AccountNumber = accountNumber;
//         this.CustomerName = customerName;
//         this.Balance = balance;
//     }

//     public List<string> TransactionHistory { get; set; } = new List<string>();

//     public void Deposit(double amount)
//     {
//         if(amount <= 0)
//         {
//             throw new InsufficientBalanceException("Deposit amount must be positive.");
//         }
//         Balance += amount;
//         TransactionHistory.Add($"Deposited {amount} on {DateTime.Now}");
//     }

//     public void Withdraw(double amount)
//     {

//         Balance -= amount;
//     }

//     public abstract double CalculateInterest()
// }

// public class SavingsAccount : BankAccount
// {

// }

// public class CurrentAccount : BankAccount
// {

// }

// public class LoanAccount : BankAccount
// {

// }