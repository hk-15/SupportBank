using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;


internal class Program
{
    public static void Main(string[] args)
    {
        var transactions = SupportBank.GetTransactions();
        var accounts = SupportBank.CreatingAccounts(transactions);
        SupportBank.ListAll(accounts, transactions);

        // string? userInput = Console.ReadLine();
        // while (userInput == null) {
        //     Console.WriteLine("Please enter a valid name.");
        //     userInput = Console.ReadLine();
        // }
        SupportBank.ListAccount("Chris W",transactions);
         
    }
}


