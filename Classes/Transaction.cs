using CsvHelper.Configuration;
using NLog;

public class Transaction
{
    public DateOnly Date { get; set; }
    public string? From { get; set; }
    public string? To { get; set; }
    public string? Narrative { get; set; }
    public float Amount {get; set; }

    public int RowNumber {get; set; }

    // public Transaction(float amount)
    // {
    //     if (amount != float)
    //     Amount = amount;
    //  }
}

public class TransactionMap : ClassMap<Transaction>
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

    public TransactionMap()
    {
        try {
        Map(m => m.Date);
        Map(m => m.From);
        Map(m => m.To);
        Map(m => m.Narrative);
        Map(m => m.Amount);
        Map(m => m.RowNumber).Index(0);  
        }
        catch (Exception err) {
            Logger.Info($"Error on row: {err}");
        }
        
    }
}
