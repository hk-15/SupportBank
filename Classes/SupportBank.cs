using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using NLog;
using Newtonsoft.Json;


public class SupportBank
{

    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();


    public static void GetJSONTransactions()
    {

        string fileName = "./Transactions2013.json";
        string jsonString = File.ReadAllText(fileName);        
       // Console.WriteLine(jsonString);
        var result = JsonConvert.DeserializeObject<List<JSONTransaction>>(jsonString);
        // var result = JsonConvert.DeserializeObject<JSONTransaction>(jsonString);


        //var result = JsonConvert.DeserializeObject<List<JSONTransaction>>("./Transactions2013.json");
        foreach (var item in result)
        {
            Console.WriteLine(item.Date);
        }
        

        //return result;
    }

    public static List<Transaction> GetCSVTransactions()
    {
        Logger.Info("Getting transactions...");
        string[] filePaths = ["./Transactions2014.csv", "./DodgyTransactions2015.csv"];

        var transactions = new List<Transaction>();

        foreach (var path in filePaths)
        {
            var config = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                HasHeaderRecord = true,
            };
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, config);

            while (csv.Read())
            {
                try
                {
                    var record = csv.GetRecord<Transaction>();
                    transactions.Add(record);
                }
                catch (Exception)
                {
                    int column;
                    if (csv.Parser.Context.Reader != null)
                    {
                        column = csv.Parser.Context.Reader.CurrentIndex;
                        Logger.Error($"Error at row {csv.Parser.Row} in column {column + 1} in {path}: {csv.Parser.Context.Reader.HeaderRecord?[column]} is invalid.");
                    }
                }
            }
        }
        return transactions;
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
        Logger.Info("Starting ListAll method");

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
        Logger.Info("Starting ListAccount method");

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
