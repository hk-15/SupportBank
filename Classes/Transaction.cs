using CsvHelper.Configuration;
using NLog;

public class Transaction
{
    public DateOnly Date { get; set; }
    public string? From { get; set; }
    public string? To { get; set; }
    public string? Narrative { get; set; }
    public float Amount {get; set; }
}
