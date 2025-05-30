using System.Text.Json;
using Newtonsoft.Json;

public class JSONTransaction
{
    public DateTime Date { get; set; }
    [JsonProperty("FromAccount")]
    public string? From { get; set; }
    [JsonProperty("ToAccount")]
    public string? To { get; set; }
    public string? Narrative { get; set; }
    public float Amount {get; set; }
}
