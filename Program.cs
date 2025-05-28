using System.Diagnostics;
using System.Globalization;
using System.Transactions;
using CsvHelper;

internal class Program
{
    public static void Main(string[] args)
    {

        using (var reader = new StreamReader("./Transactions2014.csv"))
        using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
        {
            var transactions = csv.GetRecords<Transaction>().ToList();
            List<string?> names = new List<string?>();

            foreach (var transaction in transactions)
            {
                names.Add(transaction.From);
                names.Add(transaction.To);
            }
            List<string?> UniqueNames = new HashSet<string?>(names).ToList();

            List<Account> accounts = new List<Account>();

            foreach (var name in UniqueNames)
            {
                accounts.Add(new Account {Name = name});
            }


        }
    }
    

public class Transaction
{
    public DateOnly Date { get; set; }
    public string? From { get; set; }
    public string? To { get; set; }
    public string? Narrative { get; set; }
    public float Amount { get; set; }
}

public class Account 
{
    public string? Name {get; set;}
    public List<Transaction> MoneyIn = new List<Transaction>();
    public List<Transaction> MoneyOut = new List<Transaction>();
    

    // public float TotalMoneyIn() 
    // {
    //     LoopExpression {
    //         if name = name 
    //         add to moeyin/moneyout
    //     }
    // }     
}

    
}


