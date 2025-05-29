using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Transactions;
using NLog;
using NLog.Config;
using NLog.Targets;



public class SupportBank
{

    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

    public static List<Transaction> GetTransactions()
    {
        Logger.Info("Getting transactions...");
        string[] filePaths = ["./Transactions2014.csv", "./DodgyTransactions2015.csv"];

        var transactions = new List<Transaction>();

        // try
        // {
        foreach (var path in filePaths)
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
            {
                csv.Context.RegisterClassMap<TransactionMap>();
                transactions.AddRange(csv.GetRecords<Transaction>().ToList());
    
                // transactions.AddRange(csv.GetRecords<Transaction>().ToList());
                Logger.Info($"Got transactions");
            }
                
            }
            return transactions;
        // }
        // catch ()
        // {

        // }

    }

    public static List<string?> GetUniqueNames(List<Transaction> transactions)
    {
        List<string?> names = new List<string?>();

        foreach (var transaction in transactions)
        {
            names.Add(transaction.From);
            names.Add(transaction.To);
        }
        List<string?> uniqueNames = new HashSet<string?>(names).ToList();
        return uniqueNames;
    }
    public static List<Account> CreateAccounts(List<string?> uniqueNames)
    {

        List<Account> accounts = new List<Account>();

        foreach (var name in uniqueNames)
        {
            accounts.Add(new Account { Name = name });
        }
        return accounts;
    }

    public static string ListAll(List<Account> accounts, List<Transaction> transactions)

    {
        string result = "";
        foreach (var account in accounts)
        {
            double totalOut = Math.Round(account.TotalMoneyOut(transactions), 2);
            double totalIn = Math.Round(account.TotalMoneyIn(transactions), 2);

            double totalBalance = Math.Round(totalIn - totalOut, 2);

            result += $"{account.Name} has given £{totalOut}, has receieved £{totalIn}. Their balance is £{totalBalance}.\n";
        }
        Console.Write(result);
        return result;

    }


    public static List<Transaction> ListAccount(string? name, List<Transaction> transactions)
    {
        List<Transaction> accountStatement = new List<Transaction>();

        foreach (var transaction in transactions)
        {
            if (transaction.From == name || transaction.To == name)
            {
                accountStatement.Add(transaction);
            }

        }
        Console.WriteLine($"Transactions for {name}:");
        foreach (var statement in accountStatement)
        {
            Console.WriteLine($"\n Date: {statement.Date}\n From: {statement.From}\n To: {statement.To}\n Narrative: {statement.Narrative}\n Amount: £{statement.Amount}\n");
            Console.WriteLine("------------");
        }
        return accountStatement;
    }
}
