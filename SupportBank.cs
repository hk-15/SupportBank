using CsvHelper;
using System.Globalization;



public class SupportBank {

    public static List<Transaction> GetTransactions() {
        using (var reader = new StreamReader("./Transactions2014.csv"))
        using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
        {
            var transactions = csv.GetRecords<Transaction>().ToList();
            return transactions;
        }
    }

    public static List<Account> CreatingAccounts(List<Transaction> transactions) {
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
        Console.WriteLine(result);
        return result;

}
}
