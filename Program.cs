using System.Globalization;
using CsvHelper;

using (var reader = new StreamReader("./Transactions2014.csv"))
using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
{
    var transactions = csv.GetRecords<Transaction>();
    foreach (var transaction in transactions)
    {
        Console.WriteLine($"Date: {transaction.Date}, From: {transaction.From}, To: {transaction.To}, Narrative: {transaction.Narrative}, Amount: {transaction.Amount}");
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
