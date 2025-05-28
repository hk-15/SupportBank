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
         
    }
}


