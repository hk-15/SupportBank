public class JSONTransaction
{
    public DateTime Date { get; set; }
    public string? FromAccount { get; set; }
    public string? ToAccount { get; set; }
    public string? Narrative { get; set; }
    public float Amount {get; set; }
}
