namespace Core.Entities;

public class JournalEntry
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public int Score { get; set; }
}
